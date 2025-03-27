using _Project.CodeBase.Runtime.StateMachine;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Entrypoints
{
    public class BootEntrypoint : MonoBehaviour
    {
        private AppStateMachine _stateMachine;

        [Inject]
        private void Construct(AppStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void Start()
        {
            _stateMachine.Initialize();
        }
    }
}