using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.UI.MainWindow
{
    public class MainWindowFactory : Factories.Interfaces.IFactory<MainWindowPresenter>
    {
        private const string PrefabKey = "Mainwindow";
        
        private readonly UIElementsProvider _uiElementsProvider;
        private readonly Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _goFactory;
        private readonly DiContainer _container;

        public MainWindowFactory(UIElementsProvider uiElementsProvider,
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> goFactory,
            DiContainer container)
        {
            _uiElementsProvider = uiElementsProvider;
            _goFactory = goFactory;
            _container = container;
        }

        public MainWindowPresenter Create()
        {
            var viewPrefab = _uiElementsProvider.FindElement(PrefabKey);
            var view = _goFactory.Create(viewPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), _uiElementsProvider.GetCurrentRoot());
            _container.Bind<MainWindowView>().FromInstance(view.GetComponentInChildren<MainWindowView>()).AsSingle();
            MainWindowPresenter presenter = _container.Instantiate<MainWindowPresenter>();
            presenter.InitializeUnit();
            _container.Bind<MainWindowPresenter>().FromInstance(presenter).AsSingle();
            return presenter;
        }
    }
}