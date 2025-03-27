using _Project.CodeBase.Runtime.SceneManagement.Interfaces;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Runtime.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, mode);
        }
    }
}