using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UI.ProfileWindow
{
    public class ProfileWindowPresenter : IInitializable
    {
        public bool WasInitialized { get; private set; }
        
        private readonly Authenticator _authenticator;
        private readonly ProfileWindowView _view;
        
        public ProfileWindowPresenter(Authenticator authenticator, ProfileWindowView view)
        {
            _authenticator = authenticator;
            _view = view;
        }

        public void InitializeUnit()
        {
            WasInitialized = true;
            Initialize();
        }

        private void Initialize()
        {
            _authenticator.OnNewAuthentication += (_) => OpenProfileWindow();
            _authenticator.OnLogout += OpenStartWindow;
            if (_authenticator.isAuthenticated) OpenProfileWindow();
            else OpenStartWindow();
        }

        private void OpenStartWindow()
        {
            OpenPanel(ProfilePanelId.Start);
            
            _view.LoginBtn.onClick.AddListener(() =>
            { OpenAuthenticateWindow(false); });
            
            _view.RegisterBtn.onClick.AddListener(() =>
            { OpenAuthenticateWindow(true); });
            
            _view.CloseStartWindowBtn.onClick.AddListener(() =>
            { GameObject.Destroy(_view.gameObject); });
        }

        private void OpenAuthenticateWindow(bool isRegistering)
        {
            OpenPanel(ProfilePanelId.Authenticate);
                
            _view.AuthenticateBtn.onClick.AddListener(async () =>
            {
                if (isRegistering) await _authenticator.Register(_view.LoginField.text, _view.PasswordField.text);
                else _authenticator.Login(_view.LoginField.text, _view.PasswordField.text);
            });

            var authenticateMethodName = isRegistering ? "register" : "login";

            _view.AuthenticateLabel.text = authenticateMethodName;
            _view.AuthenticateButtonLabel.text = authenticateMethodName;
            
            _view.CloseAuthenticationWindowBtn.onClick.AddListener(() =>
            { GameObject.Destroy(_view.gameObject); });
        }

        private void OpenProfileWindow()
        {
            OpenPanel(ProfilePanelId.Profile);
            
            _view.LoginLabel.text = _authenticator.User.Login;
            
            _view.LogoutBtn.onClick.AddListener(() =>
            { _authenticator.Logout(); });
            
            _view.CloseProfileWindowBtn.onClick.AddListener(() =>
            { GameObject.Destroy(_view.gameObject); });
        }

        private void OpenPanel(ProfilePanelId id)
        {
            bool openStart = id == ProfilePanelId.Start;
            bool openAuthenticate = id == ProfilePanelId.Authenticate;
            bool openProfile = id == ProfilePanelId.Profile;
            
            _view.StartPanel.SetActive(openStart);
            _view.AuthenticatePanel.SetActive(openAuthenticate);
            _view.ProfilePanel.SetActive(openProfile);
        }

        private enum ProfilePanelId
        {
            Start,
            Authenticate,
            Profile
        }
    }
}