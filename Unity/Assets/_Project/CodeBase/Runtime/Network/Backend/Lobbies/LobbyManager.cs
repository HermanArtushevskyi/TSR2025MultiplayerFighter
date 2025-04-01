using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Models;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using _Project.CodeBase.Runtime.Network.Mirror;
using _Project.CodeBase.Runtime.SceneManagement.Interfaces;
using Cysharp.Threading.Tasks;
using Mirror;
using Mirror.Discovery;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.CodeBase.Runtime.Network.Backend.Lobbies
{
    public class LobbyManager : BackendAccessor
    {
        public event Action OnConnectedToLobby;
        public event Action OnDisconnectedFromLobby;
        
        private readonly Authenticator _authenticator;
        private readonly FighterNetworkManager _networkManager;
        private readonly ISceneLoader _sceneLoader;

        private string _lastLobbyNameRequest;
        private string _currentLobbyName;
        
        public LobbyManager(BackendSettings backendSettings, Authenticator authenticator, FighterNetworkManager networkManager
            , ISceneLoader sceneLoader)
            : base(backendSettings)
        {
            _sceneLoader = sceneLoader;
            _authenticator = authenticator;
            _networkManager = networkManager;
            _currentLobbyName = "";
            _networkManager.OnHostReady += () => OnHostReady();
        }
        
        public async UniTask<List<Lobby>> GetAllLobbies()
        {
            var lobbies = new List<Lobby>();
            var request = UnityWebRequest.Get(GetURI(BackendSettings.AllLobbiesPath));
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                lobbies = JsonConvert.DeserializeObject<List<Lobby>>(request.downloadHandler.text);
                return lobbies;
            }
            
            return null;
        }

        public async UniTaskVoid ConnectToLobby(string name)
        {
            var request = UnityWebRequest.Get(
                $"{GetURI(BackendSettings.JoinLobbyPath)}?name={name}&secondUserLogin={_authenticator.User.Login}");
            
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                _networkManager.StartClient();
                _currentLobbyName = name;
                OnConnectedToLobby?.Invoke();
            }
            else Debug.Log($"Error while connection to lobby! {name}");
        }

        public async UniTaskVoid Disconnect()
        {
            OnDisconnectedFromLobby?.Invoke();
            _currentLobbyName = "";
            var request = UnityWebRequest.Get(
                $"{GetURI(BackendSettings.LeaveLobbyPath)}?userLogin={_authenticator.User.Login}");
            await request.SendWebRequest();
        }

        public async UniTask CreateLobby(string name)
        {
            _lastLobbyNameRequest = name;
            _networkManager.StartHost();
        }

        private void OnHostReady()
        {
            try
            {
                string address = "localhost:7777";
                Debug.Log($"Trying to send request! Lobby create {address}");
                var request = UnityWebRequest.Get(
                    $"{GetURI(BackendSettings.CreateLobbyPath)}?name={_lastLobbyNameRequest}&firstUserLogin={_authenticator.User.Login}&address={address}");
                request.SendWebRequest();
            }
            catch
            {
                Debug.Log("Some mistake while creating lobby");
            }

        }
    }
}