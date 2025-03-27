using _Project.CodeBase.Runtime.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UI.MessagePopup
{
    public class MessagePopupPresenter : IInitializable
    {
        public bool WasInitialized { get; private set; }
        
        private readonly MessagePopupView _view;
        private readonly string _text;

        public MessagePopupPresenter(MessagePopupView view, string text)
        {
            _view = view;
            _text = text;
        }

        public void InitializeUnit()
        {
            WasInitialized = true;
            Initialize();
        }

        private void Initialize()
        {
            _view.MessageText.text = _text;
            
            _view.CloseBtn.onClick.AddListener(() =>
            {
                GameObject.Destroy(_view.gameObject);
            });
        }
    }
}