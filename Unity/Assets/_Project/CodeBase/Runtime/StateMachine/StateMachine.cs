using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;

namespace _Project.CodeBase.Runtime.StateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        protected Dictionary<Type, IExitState> ExitStates;
        protected IExitState CurrentState;

        public bool WasInitialized { get; protected set; }
        public void InitializeUnit()
        {
            Initialize();
            WasInitialized = true;
        }

        public abstract void Initialize();

        public virtual void ChangeState<TState>() where TState : class, IState
        {
            if (ExitStates.TryGetValue(typeof(TState), out var state))
            {
                CurrentState?.Exit();
                CurrentState = state;
                ((IState) CurrentState).Enter();
            }
        }

        public virtual void ChangeState<TState, TArg>(TArg arg) where TState : class, IState<TArg>
        {
            if (ExitStates.TryGetValue(typeof(TState), out var state))
            {
                CurrentState?.Exit();
                CurrentState = state;
                ((IState<TArg>) CurrentState).Enter(arg);
            }
        }
        
        protected void SetExitStates(Dictionary<Type, IExitState> exitStates)
        {
            ExitStates = exitStates;
        }
    }
}