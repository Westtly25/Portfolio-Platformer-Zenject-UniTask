using Cysharp.Threading.Tasks;

namespace Assets.Code.Scripts.Runtime.Fsm
{
    public interface IState
    {
        StateMachine StateMachine { get; set; }
        UniTask OnEnter();
        UniTask OnExit();
        void SetOptions(Options options);
        void OnUpdate();
    }
}
