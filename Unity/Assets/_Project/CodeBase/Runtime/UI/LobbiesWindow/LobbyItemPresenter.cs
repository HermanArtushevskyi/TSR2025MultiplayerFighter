using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Models;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using _Project.CodeBase.Runtime.Network.Backend.Lobbies;

namespace _Project.CodeBase.Runtime.UI.LobbiesWindow
{
    public class LobbyItemPresenter : IInitializable
    {
        public bool WasInitialized { set; get; }
        
        private readonly Lobby _lobby;
        private readonly LobbyItemView _view;
        private readonly LobbyManager _lobbyManager;
        private readonly Authenticator _authenticator;

        public LobbyItemPresenter(Lobby lobby, LobbyItemView view, LobbyManager lobbyManager, Authenticator authenticator)
        {
            _lobby = lobby;
            _view = view;
            _lobbyManager = lobbyManager;
            _authenticator = authenticator;
        }

        public void InitializeUnit()
        {
            WasInitialized = true;
            Initialize();
        }

        private void Initialize()
        {
            _view.NameLabel.text = _lobby.Name;
            _view.JoinBtn.onClick.AddListener(OnLobbyClick);
        }

        private void OnLobbyClick()
        {
            _lobbyManager.ConnectToLobby(_lobby.Name);
        }
    }
}