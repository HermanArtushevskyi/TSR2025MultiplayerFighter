using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.SceneManagement.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine
{
    public class AppStateMachine : StateMachine
    {
        public AppStateMachine(ISceneLoader sceneLoader)
        {
            SetExitStates(new Dictionary<Type, IExitState>
            {
                {typeof(BootState), new BootState(this)},
                {typeof(SwitchToMenuState), new SwitchToMenuState(sceneLoader)},
                {typeof(ExitAppState), new ExitAppState()}
            });
        }

        public override void Initialize()
        {
            ChangeState<BootState>();
        }

        public class BootState : IState
        {
            private readonly IStateMachine _stateMachine;

            public BootState(IStateMachine stateMachine)
            {
                _stateMachine = stateMachine;
            }

            public void Enter()
            {
                _stateMachine.ChangeState<SwitchToMenuState>();
            }

            public void Exit()
            {
                
            }
        }
        
        public class SwitchToMenuState : IState
        {
            private readonly ISceneLoader _sceneLoader;

            public SwitchToMenuState(ISceneLoader sceneLoader)
            {
                _sceneLoader = sceneLoader;
            }

            public void Enter()
            {
                _sceneLoader.LoadScene("MenuScene");
            }

            public void Exit()
            {
                
            }
        }

        public class ExitAppState : IState
        {
            public void Enter()
            {
                Debug.Log("Closing player application");
                Application.Quit();
            }

            public void Exit()
            {
            }
        }
    }
}