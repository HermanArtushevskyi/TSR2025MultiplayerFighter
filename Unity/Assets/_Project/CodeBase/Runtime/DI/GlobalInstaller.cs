using _Project.CodeBase.Runtime.Factories;
using _Project.CodeBase.Runtime.SceneManagement;
using _Project.CodeBase.Runtime.SceneManagement.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.UI;
using _Project.CodeBase.Runtime.UI.MessagePopup;
using UnityEngine;
using Zenject;
using IFactories = _Project.CodeBase.Runtime.Factories.Interfaces;

namespace _Project.CodeBase.Runtime.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<AppStateMachine>().AsSingle();
            Container.Bind<IFactories.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform>>().To<GameObjectFactory>()
                .AsSingle();
            Container.Bind<UIElementsProvider>().AsSingle();
            Container.Bind<IFactories.IFactory<MessagePopupPresenter, string>>().To<MessagePopupFactory>().AsSingle();
        }
    }
}