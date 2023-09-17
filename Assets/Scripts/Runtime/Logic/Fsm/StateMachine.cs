using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using Assets.Code.Scripts.Runtime.Fsm.Transitions;

namespace Assets.Code.Scripts.Runtime.Fsm
{
    public class StateMachine
    {
        private IState currentState;
        private IState previousState;
        private readonly Dictionary<Type, IState> states = new();
        private readonly Queue<Transition> pendingTransitions = new();
        private CancellationTokenSource cancellationTokenSource;

        public void RegisterState(IState state)
        {
            state.StateMachine = this;

            states.Add(state.GetType(), state);
        }

        public void RequestTransition(Type stateType)
        {
            pendingTransitions.Enqueue(new Transition(stateType, null));
        }

        public void RequestTransition<T>(Type stateType, T options) where T : Options
        {
            pendingTransitions.Enqueue(new Transition(stateType, options));
        }

        private async UniTask ChangeTo<T>(Type stateType, T options) where T : Options
        {
            if (currentState != null)
            {
                previousState = currentState;
                await previousState.OnExit();
                currentState = null;
            }

            if (states.TryGetValue(stateType, out IState nextState))
            {
                nextState.SetOptions(options);
                currentState = nextState;
                await nextState.OnEnter();
            }
            else
                throw new Exception($"State: {stateType.Name} is not registered to state machine.");
        }

        public void Run()
        {
            cancellationTokenSource = new();
            Update();
        }

        private async void Update()
        {
            await foreach (var _ in UniTaskAsyncEnumerable
            .EveryUpdate()
            .WithCancellation(cancellationTokenSource.Token))
            {
                while (pendingTransitions.Count > 0)
                {
                    Transition transition = pendingTransitions.Dequeue();
                    await ChangeTo(transition.Type, transition.Options);
                }

                currentState?.OnUpdate();
            }
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
