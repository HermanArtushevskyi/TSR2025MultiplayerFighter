using System;
using _Project.CodeBase.Runtime.Network.Mirror;
using Mirror;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay
{
    public class RoundManager : NetworkBehaviour
    {
        public GameObject PlayerPrefab;
        public Transform HostSpawnPoint;
        public Transform UserSpawnPoint;

        private GameObject HostPlayer;
        private GameObject UserPlayer;
        private FighterNetworkManager _manager;
        
        [Inject]
        private void Construct(FighterNetworkManager manager)
        {
            _manager = manager;
        }
    }
}