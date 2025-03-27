using UnityEngine;

namespace _Project.CodeBase.Runtime.UI.MessagePopup
{
    public class MessagePopupFactory : Factories.Interfaces.IFactory<MessagePopupPresenter, string>
    {
        private const string PrefabKey = "MessagePopup";
        private readonly UIElementsProvider _uiElementsProvider;
        private readonly Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _goFactory;
        
        public MessagePopupFactory(
            UIElementsProvider uiElementsProvider,
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> goFactory)
        {
            _uiElementsProvider = uiElementsProvider;
            _goFactory = goFactory;
        }

        public MessagePopupPresenter Create(string text)
        {
            var viewPrefab = _uiElementsProvider.FindElement(PrefabKey);
            var view = _goFactory.Create(viewPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), _uiElementsProvider.GetCurrentRoot());
            var presenter = new MessagePopupPresenter(view.GetComponentInChildren<MessagePopupView>(), text);
            presenter.InitializeUnit();
            return presenter;
        }
    }
}