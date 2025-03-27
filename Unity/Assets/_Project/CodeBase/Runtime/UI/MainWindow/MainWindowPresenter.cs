using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.UI.MessagePopup;
using IInitializable = _Project.CodeBase.Runtime.Common.IInitializable;

namespace _Project.CodeBase.Runtime.UI.MainWindow
{
    public class MainWindowPresenter : IInitializable
    {
        public bool WasInitialized { get; private set; }
        
        private readonly MainWindowView _view;
        private readonly AppStateMachine _appStateMachine;
        private readonly IFactory<MessagePopupPresenter, string> _messagePopupFactory;

        private MainWindowPresenter(
            AppStateMachine appStateMachine,
            MainWindowView view,
            IFactory<MessagePopupPresenter, string> messagePopupFactory)
        {
            _appStateMachine = appStateMachine;
            _view = view;
            _messagePopupFactory = messagePopupFactory;
        }
        
        public void InitializeUnit()
        {
            Initialize();
            WasInitialized = true;
        }

        private void Initialize()
        {
            _view.ExitBtn.onClick.AddListener(() =>
            {
                _appStateMachine.ChangeState<AppStateMachine.ExitAppState>();
            });
            
            _view.LobbiesBtn.onClick.AddListener(() =>
            {
                _messagePopupFactory.Create("Lobbies button clicked");
            });
        }
    }
}