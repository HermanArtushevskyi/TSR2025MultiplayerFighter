using System;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using _Project.CodeBase.Runtime.Network.Backend.Lobbies;
using Mirror;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Network.Mirror
{
    public class FighterNetworkManager : NetworkManager
    {
        public event Action OnHostReady;
        public event Action OnConnectedToHost;
        public event Action OnPlayerConnected;
        
        private LobbyManager _lobbyManager;
        private Authenticator _authenticator;
        
        private int id = 0;

        [Inject]
        public void Construct(LobbyManager lobbyManager, Authenticator authenticator)
        {
            _lobbyManager = lobbyManager;
            _authenticator = authenticator;
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            base.OnServerConnect(conn);
            
            OnPlayerConnected?.Invoke();
        }

        public override void OnStartHost()
        {
            base.OnStartHost();
            OnHostReady?.Invoke();
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Debug.Log($"Trying to add player {id}");
            Debug.Log($"Found player go {GameObject.FindWithTag("Host").name}");
            string tag = id == 0 ? "Host" : "User";
            NetworkServer.AddPlayerForConnection(conn, GameObject.FindWithTag(tag));
            id++;
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            OnConnectedToHost?.Invoke();
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            _lobbyManager.Disconnect();
        }
    }
}