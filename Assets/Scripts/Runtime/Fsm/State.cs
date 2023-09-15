using Cysharp.Threading.Tasks;

namespace Assets.Code.Scripts.Runtime.Fsm
{
    public abstract class State<T> : IState where T : Options
    {
        public T Options { get; private set; }
        public StateMachine StateMachine { get; set; }

        public virtual async UniTask OnEnter()
        {
            await UniTask.Yield();
        }

        public virtual async UniTask OnExit()
        {
            await UniTask.Yield();
        }

        public void SetOptions(Options options)
        {
            if (options is T stateOptions)
            {
                Options = stateOptions;
            }
        }

        public virtual void OnUpdate()
        {
        }
    }
}
