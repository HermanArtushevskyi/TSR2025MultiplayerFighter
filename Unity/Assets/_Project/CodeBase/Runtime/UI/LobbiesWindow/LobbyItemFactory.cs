using _Project.CodeBase.Runtime.Models;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.UI.LobbiesWindow
{
    public class LobbyItemFactory : Factories.Interfaces.IFactory<LobbyItemPresenter, Transform, Lobby>
    {
        private const string PrefabKey = "LobbyItem";
        
        private readonly UIElementsProvider _elementsProvider;
        private readonly Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _goFactory;
        private readonly DiContainer _container;

        public LobbyItemFactory(
            UIElementsProvider elementsProvider,
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> goFactory,
            DiContainer container)
        {
            _elementsProvider = elementsProvider;
            _goFactory = goFactory;
            _container = container;
        }

        public LobbyItemPresenter Create(Transform lobbiesContent, Lobby lobby)
        {
            var viewPrefab = _elementsProvider.FindElement(PrefabKey);
            var view = _goFactory.Create(viewPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), lobbiesContent);
            var presenter = _container.Instantiate<LobbyItemPresenter>(new object[] {view.GetComponent<LobbyItemView>(), lobby});
            presenter.InitializeUnit();
            return presenter;
        }
    }
}