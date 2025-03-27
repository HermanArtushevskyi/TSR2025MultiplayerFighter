using _Project.CodeBase.Runtime.UI;
using _Project.CodeBase.Runtime.UI.MainWindow;
using UnityEngine;
using Zenject;
using IFactories = _Project.CodeBase.Runtime.Factories.Interfaces;

namespace _Project.CodeBase.Runtime.Entrypoints
{
    public class MainMenuEntrypoint : MonoBehaviour
    {
        private IFactories.IFactory<MainWindowPresenter> _mainWindowFactory;
        private UIElementsProvider _uiElementsProvider;
        private Transform _canvasRoot;

        [Inject]
        private void Construct(
            IFactories.IFactory<MainWindowPresenter> mainWindowFactory,
            UIElementsProvider uiElementsProvider,
            Transform canvasRoot)
        {
            _mainWindowFactory = mainWindowFactory;
            _uiElementsProvider = uiElementsProvider;
            _canvasRoot = canvasRoot;
        }
        
        private void Start()
        {
            _uiElementsProvider.SetCurrentRoot(_canvasRoot);
            _mainWindowFactory.Create();
        }
    }
}