using _Project.CodeBase.Runtime.Common;

namespace _Project.CodeBase.Runtime.StateMachine.Interfaces
{
    public interface IStateMachine : IInitializable
    {
        public void Initialize();
        public void ChangeState<TState>() where TState : class, IState;
        public void ChangeState<TState, TArg>(TArg arg) where TState : class, IState<TArg>;
    }
}