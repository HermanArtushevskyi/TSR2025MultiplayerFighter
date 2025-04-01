using System.Collections;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Common
{
    public class CoroutineProcessor
    {
        private readonly MonoBehaviour _behaviour;

        public CoroutineProcessor(MonoBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public void StartCoroutine(IEnumerator enumerator) => _behaviour.StartCoroutine(enumerator);
    }
}