using System;
using _Project.CodeBase.Runtime.Models;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.CodeBase.Runtime.Network.Backend.Account
{
    public class Authenticator : BackendAccessor
    {
        public event Action<User> OnNewAuthentication;
        public event Action OnLogout;

        public bool isAuthenticated { get; private set; }
        public User User { get; private set; }
        public AuthenticationCode AuthenticationCode { get; private set; }

        public Authenticator(BackendSettings settings) : base(settings)
        {
            User = new();
            AuthenticationCode = new();
        }

        public async UniTask<bool> Login(string login, string password)
        {
            var request =
                UnityWebRequest.Get(
                    $"{GetURI(BackendSettings.LoginPath)}?login={login}&password={password}");
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var userRequest =
                    UnityWebRequest.Get(
                        $"{GetURI(BackendSettings.GetUserPath)}?&authenticationCode={request.downloadHandler.text}");
                await userRequest.SendWebRequest();
                if (userRequest.result == UnityWebRequest.Result.Success)
                {
                    isAuthenticated = true;
                    Debug.Log(userRequest.downloadHandler.text);
                    Models.User user = JsonConvert.DeserializeObject<User>(userRequest.downloadHandler.text);
                    User.Login = user.Login;
                    AuthenticationCode.Value = request.downloadHandler.text;
                    OnNewAuthentication?.Invoke(User);
                    return true;
                }
            }

            return false;
        }

        public async UniTask<bool> Register(string login, string password)
        {
            var request =
                UnityWebRequest.Get(
                    $"{GetURI(BackendSettings.RegisterPath)}?login={login}&password={password}");
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var userRequest =
                    UnityWebRequest.Get(
                        $"{GetURI(BackendSettings.GetUserPath)}?&authenticationCode={request.downloadHandler.text}");
                await userRequest.SendWebRequest();
                if (userRequest.result == UnityWebRequest.Result.Success)
                {
                    isAuthenticated = true;
                    Debug.Log(userRequest.downloadHandler.text);
                    Models.User user = JsonConvert.DeserializeObject<User>(userRequest.downloadHandler.text);
                    User.Login = user.Login;
                    OnNewAuthentication?.Invoke(User);
                    AuthenticationCode.Value = request.downloadHandler.text;
                    PlayerPrefs.SetString(nameof(AuthenticationCode), AuthenticationCode.Value);
                    return true;
                }
            }

            return false;
        }

        public async UniTask<bool> Reauthenticate()
        {
            if (PlayerPrefs.HasKey(nameof(AuthenticationCode)) == false) return false;
            
            var userRequest =
                UnityWebRequest.Get(
                    $"{GetURI(BackendSettings.GetUserPath)}?&authenticationCode={PlayerPrefs.GetString(nameof(AuthenticationCode))}");
            await userRequest.SendWebRequest();
            if (userRequest.result == UnityWebRequest.Result.Success)
            {
                isAuthenticated = true;
                Debug.Log(userRequest.downloadHandler.text);
                Models.User user = JsonConvert.DeserializeObject<User>(userRequest.downloadHandler.text);
                User.Login = user.Login;
                AuthenticationCode.Value = PlayerPrefs.GetString(nameof(AuthenticationCode));
                OnNewAuthentication?.Invoke(User);
                return true;
            }

            return false;
        }
        
        public async UniTaskVoid Logout()
        {
            isAuthenticated = false;
            AuthenticationCode = new AuthenticationCode();
            User = new User();
            OnLogout?.Invoke();
            PlayerPrefs.DeleteKey(nameof(AuthenticationCode));
            var request =
                UnityWebRequest.Get(
                    $"{GetURI(BackendSettings.LogoutPath)}?authenticationCode={AuthenticationCode}");
            await request.SendWebRequest();
        }
    }
}