using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.UI.LobbiesWindow
{
    public class LobbiesWindowView : MonoBehaviour
    {
        public GameObject LobbiesContent;
        public GameObject LobbyPrefab;
        public TMP_InputField LobbyNameField;
        public Button CreateLobbyBtn;
        public Button RefreshBtn;
        public Button ClsoeLobbyWindowBtn;
        public GameObject WaitForPlayerPanel;
        public Button DestroyLobby;
    }
}