using System;

namespace _Project.CodeBase.Runtime.Network.Backend
{
    [Serializable]
    public struct BackendSettings
    {
        // Server
        public string Address;
        public string Port;
        
        // Users
        public string RegisterPath;
        public string LoginPath;
        public string LogoutPath;
        public string GetUserPath;

        // Lobbies
        public string AllLobbiesPath;
        public string LobbyByNamePath;
        public string CreateLobbyPath;
        public string JoinLobbyPath;
        public string LeaveLobbyPath;
        public string DeleteLobbyPath;
    }
}