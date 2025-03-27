using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Runtime.SceneManagement.Interfaces
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
    }
}