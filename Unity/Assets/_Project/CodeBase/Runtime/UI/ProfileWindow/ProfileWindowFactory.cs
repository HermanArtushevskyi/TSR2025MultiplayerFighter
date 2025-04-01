using _Project.CodeBase.Runtime.UI.MainWindow;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.UI.ProfileWindow
{
    public class ProfileWindowFactory : Factories.Interfaces.IFactory<ProfileWindowPresenter>
    {
        private const string PrefabKey = "ProfileWindow";
        
        private readonly UIElementsProvider _uiElementsProvider;
        private readonly Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _goFactory;
        private readonly DiContainer _container;

        public ProfileWindowFactory(UIElementsProvider uiElementsProvider,
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> goFactory,
            DiContainer _container)
        {
            _uiElementsProvider = uiElementsProvider;
            _goFactory = goFactory;
            this._container = _container;
        }

        public ProfileWindowPresenter Create()
        {
            var viewPrefab = _uiElementsProvider.FindElement(PrefabKey);
            var view = _goFactory.Create(viewPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), _uiElementsProvider.GetCurrentRoot());
            ProfileWindowPresenter presenter = _container.Instantiate<ProfileWindowPresenter>(new []{view.GetComponentInChildren<ProfileWindowView>()});
            presenter.InitializeUnit();
            return presenter;
        }
    }
}