using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.UI
{
    public class MainMenuEventHandler : MonoBehaviour
    {
        #region Fields
        [SerializeField] int levelSceneBuildIndex;
        #endregion

        #region Methods
        public void OnPlayButtonClick() => SceneManager.LoadScene(levelSceneBuildIndex);
        public void OnExitButtonClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.Quit();
            Application.OpenURL("https://yuriy-danyliuk.itch.io/");
#else
            Application.Quit();
#endif
        }
        #endregion
    }
}