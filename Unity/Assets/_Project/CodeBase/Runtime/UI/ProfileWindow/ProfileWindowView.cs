using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.UI.ProfileWindow
{
    public class ProfileWindowView : MonoBehaviour
    {
        [Header("StartWindow")]
        public GameObject StartPanel;
        public Button LoginBtn;
        public Button RegisterBtn;
        public Button CloseStartWindowBtn;
        [Header("AuthenticationWindow")]
        public GameObject AuthenticatePanel;
        public TMP_InputField LoginField;
        public TMP_InputField PasswordField;
        public Button AuthenticateBtn;
        public Button CloseAuthenticationWindowBtn;
        public TextMeshProUGUI AuthenticateLabel;
        public TextMeshProUGUI AuthenticateButtonLabel;
        [Header("ProfileWindow")]
        public GameObject ProfilePanel;
        public TextMeshProUGUI LoginLabel;
        public Button LogoutBtn;
        public Button CloseProfileWindowBtn;
    }
}