using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Factories
{
    public class GameObjectFactory : Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform>
    {
        private readonly DiContainer _container;

        public GameObjectFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = GameObject.Instantiate(prefab, position, rotation, parent);
            return obj;
        }
    }
}