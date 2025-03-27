using UnityEngine;

namespace _Project.CodeBase.Runtime.UI
{
    public class UIElementsProvider
    {
        private Transform _root;

        public GameObject FindElement(string key)
        {
            return Resources.Load<GameObject>($"UI/{key}");
        }

        public Transform GetCurrentRoot()
        {
            return _root;
        }
        
        public void SetCurrentRoot(Transform root)
        {
            _root = root;
        }
    }
}