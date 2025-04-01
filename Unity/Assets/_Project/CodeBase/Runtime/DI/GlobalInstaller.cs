using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Factories;
using _Project.CodeBase.Runtime.Network.Backend;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using _Project.CodeBase.Runtime.Network.Backend.Lobbies;
using _Project.CodeBase.Runtime.Network.Mirror;
using _Project.CodeBase.Runtime.SceneManagement;
using _Project.CodeBase.Runtime.SceneManagement.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.UI;
using _Project.CodeBase.Runtime.UI.MessagePopup;
using kcp2k;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using Zenject;
using IFactories = _Project.CodeBase.Runtime.Factories.Interfaces;

namespace _Project.CodeBase.Runtime.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private BackendSettings _backendSettings;
        [SerializeField] private FighterNetworkManager _networkManager;
        [SerializeField] private KcpTransport _transport;
         
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<AppStateMachine>().AsSingle();
            Container.Bind<IFactories.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform>>().To<GameObjectFactory>()
                .AsSingle();
            Container.Bind<UIElementsProvider>().AsSingle();
            Container.Bind<IFactories.IFactory<MessagePopupPresenter, string>>().To<MessagePopupFactory>().AsSingle();
            Container.Bind<BackendSettings>().FromInstance(_backendSettings).AsSingle();
            Container.Bind<Authenticator>().AsSingle();
            Container.Bind<LobbyManager>().AsSingle();
            Container.Bind(typeof(FighterNetworkManager), typeof(NetworkManager)).FromInstance(_networkManager)
                .AsSingle();
            Container.Bind(typeof(KcpTransport), typeof(Transport)).FromInstance(_transport).AsSingle();
            Container.Bind<CoroutineProcessor>().FromInstance(new CoroutineProcessor(this)).AsSingle();
        }
    }
}