using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.UI.LobbiesWindow;
using _Project.CodeBase.Runtime.UI.MessagePopup;
using _Project.CodeBase.Runtime.UI.ProfileWindow;
using IInitializable = _Project.CodeBase.Runtime.Common.IInitializable;

namespace _Project.CodeBase.Runtime.UI.MainWindow
{
    public class MainWindowPresenter : IInitializable
    {
        public bool WasInitialized { get; private set; }
        
        private readonly MainWindowView _view;
        private readonly Authenticator _authenticator;
        private readonly AppStateMachine _appStateMachine;
        private readonly IFactory<MessagePopupPresenter, string> _messagePopupFactory;
        private readonly IFactory<ProfileWindowPresenter> _profileWindowFactory;
        private readonly IFactory<LobbiesWindowPresenter> _lobbiesWindowFactory;

        private MainWindowPresenter(
            AppStateMachine appStateMachine,
            MainWindowView view,
            Authenticator authenticator,
            IFactory<MessagePopupPresenter, string> messagePopupFactory,
            IFactory<ProfileWindowPresenter> profileWindowFactory,
            IFactory<LobbiesWindowPresenter> lobbiesWindowFactory)
        {
            _appStateMachine = appStateMachine;
            _view = view;
            _authenticator = authenticator;
            _messagePopupFactory = messagePopupFactory;
            _profileWindowFactory = profileWindowFactory;
            _lobbiesWindowFactory = lobbiesWindowFactory;
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
            
            _view.ProfileBtn.onClick.AddListener(() =>
            {
                _profileWindowFactory.Create();
            });
            
            _view.LobbiesBtn.onClick.AddListener(() =>
            {
                if (_authenticator.isAuthenticated == false)
                {
                    _messagePopupFactory.Create("Log in to play!");
                    return;
                }

                _lobbiesWindowFactory.Create();
            });
        }
    }
}