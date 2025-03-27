namespace _Project.CodeBase.Runtime.StateMachine.Interfaces
{
    public interface IState : IExitState
    {
        public void Enter();
    }

    public interface IState<T> : IExitState
    {
        public void Enter(T payload);
    }
}