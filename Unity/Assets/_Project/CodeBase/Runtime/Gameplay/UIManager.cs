using System;
using _Project.CodeBase.Runtime.Network.Mirror;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        public GameObject[] HostHearts;
        public GameObject[] PlayerHearts;
        public PlayerBehaviour Host;
        public PlayerBehaviour User;
        public GameObject MenuPanel;
        public GameObject GameOverPanel;
        public Button LeaveBtn;
        
        private PlayInputActions _actions;
        private FighterNetworkManager _manager;

        [Inject]
        private void Construct(FighterNetworkManager manager)
        {
            _manager = manager;
        }
        
        private void Awake()
        {
            _actions = new PlayInputActions();
            _actions.Enable();
            _actions.Player.Esc.performed += ctx => MenuPanel.SetActive(!MenuPanel.activeSelf);
            LeaveBtn.onClick.AddListener(OnLeave);
        }

        private void OnLeave()
        {
            if (NetworkServer.active) _manager.StopHost();
            else _manager.StopClient();
        }

        private void Update()
        {
            if (Host.health < 3) HostHearts[2].SetActive(false);
            if (Host.health < 2) HostHearts[1].SetActive(false);
            if (Host.health < 1)
            {
                HostHearts[0].SetActive(false);
                GameOverPanel.SetActive(true);
            };
            if (User.health < 3) PlayerHearts[2].SetActive(false);
            if (User.health < 2) PlayerHearts[1].SetActive(false);
            if (User.health < 1)
            {
                PlayerHearts[0].SetActive(false);
                GameOverPanel.SetActive(true);
            };
        }
    }
}