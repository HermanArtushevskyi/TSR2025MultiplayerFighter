using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.UI.LobbiesWindow
{
    public class LobbiesWindowFactory : Factories.Interfaces.IFactory<LobbiesWindowPresenter>
    {
        private const string PrefabKey = "LobbiesWindow";
        
        private readonly UIElementsProvider _elementsProvider;
        private readonly Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _goFactory;
        private readonly DiContainer _container;

        public LobbiesWindowFactory(
            UIElementsProvider elementsProvider,
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> goFactory,
            DiContainer container)
        {
            _elementsProvider = elementsProvider;
            _goFactory = goFactory;
            _container = container;
        }

        public LobbiesWindowPresenter Create()
        {
            var viewPrefab = _elementsProvider.FindElement(PrefabKey);
            var view = _goFactory.Create(viewPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), _elementsProvider.GetCurrentRoot());
            var presenter = _container.Instantiate<LobbiesWindowPresenter>(new object[] {view.GetComponent<LobbiesWindowView>()});
            presenter.InitializeUnit();
            return presenter;
        }
    }
}