using _Project.CodeBase.Runtime.Models;
using _Project.CodeBase.Runtime.UI.LobbiesWindow;
using _Project.CodeBase.Runtime.UI.MainWindow;
using _Project.CodeBase.Runtime.UI.ProfileWindow;
using UnityEngine;
using Zenject;
using IFactories = _Project.CodeBase.Runtime.Factories.Interfaces;

namespace _Project.CodeBase.Runtime.DI.MainMenuScene
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _canvasRoot;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(_canvasRoot).AsSingle();
            Container.Bind<IFactories.IFactory<MainWindowPresenter>>().To<MainWindowFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<ProfileWindowPresenter>>().To<ProfileWindowFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<LobbyItemPresenter, Transform, Lobby>>().To<LobbyItemFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<LobbiesWindowPresenter>>().To<LobbiesWindowFactory>().AsSingle();
        }
    }
}