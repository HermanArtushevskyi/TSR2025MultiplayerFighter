using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Models;
using _Project.CodeBase.Runtime.Network.Backend.Account;
using _Project.CodeBase.Runtime.Network.Backend.Lobbies;
using _Project.CodeBase.Runtime.Network.Mirror;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UI.LobbiesWindow
{
    public class LobbiesWindowPresenter : IInitializable
    {
        public bool WasInitialized { get; set; }
        
        private event Action<List<Lobby>> OnLobbiesReceived;
        private readonly LobbyManager _lobbyManager;
        private readonly Authenticator _authenticator;
        private readonly LobbiesWindowView _view;
        private readonly FighterNetworkManager _networkManager;
        private readonly IFactory<LobbyItemPresenter, Transform, Lobby> _lobbyItemFactory;

        private List<Lobby> _lobbies;
        
        public LobbiesWindowPresenter(
            LobbyManager lobbyManager,
            Authenticator authenticator,
            LobbiesWindowView view,
            FighterNetworkManager networkManager,
            IFactory<LobbyItemPresenter, Transform, Lobby> lobbyItemFactory)
        {
            _lobbyManager = lobbyManager;
            _authenticator = authenticator;
            _view = view;
            _networkManager = networkManager;
            _lobbyItemFactory = lobbyItemFactory;
        }

        public void InitializeUnit()
        {
            WasInitialized = true;
            Initialize();
        }

        private async void Initialize()
        {
            OnLobbiesReceived += lobbies =>
            {
                for (int childIndex = 0; childIndex < _view.LobbiesContent.transform.childCount; childIndex++)
                {
                    var child = _view.LobbiesContent.transform.GetChild(childIndex);
                    GameObject.Destroy(child.gameObject);
                }

                foreach (Lobby lobby in lobbies)
                {
                    _lobbyItemFactory.Create(_view.LobbiesContent.transform, lobby);
                }
            };
            await GetAllLobbies();
            _view.RefreshBtn.onClick.AddListener( () => _ = OnRefreshClick());
            _view.ClsoeLobbyWindowBtn.onClick.AddListener(() => GameObject.Destroy(_view.gameObject));
            _view.CreateLobbyBtn.onClick.AddListener(OnCreateClick);
            _view.DestroyLobby.onClick.AddListener(() => OnDestroyLobby());
        }


        private async UniTask<List<Lobby>> GetAllLobbies()
        {
            var allLobbies =  await _lobbyManager.GetAllLobbies();
            OnLobbiesReceived?.Invoke(allLobbies);
            _lobbies = allLobbies;
            return allLobbies;
        }

        private void OnCreateClick()
        {
            _view.WaitForPlayerPanel.gameObject.SetActive(true);
            _lobbyManager.CreateLobby(_view.LobbyNameField.text);
        }

        private async UniTaskVoid OnRefreshClick()
        {
            await GetAllLobbies();
        }

        private async UniTaskVoid OnDestroyLobby()
        { 
            _lobbyManager.Disconnect();
            _view.WaitForPlayerPanel.gameObject.SetActive(false);
        }
    }
}