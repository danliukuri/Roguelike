using Roguelike.UI.Information;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Roguelike.UI.Events.Handling
{
    public class MainMenuEventHandler : MonoBehaviour
    {
        #region Fields
        MenusInfo menusInfo;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(MenusInfo menusInfo) => this.menusInfo = menusInfo;
        
        public void OnPlayButtonClick()
        {
            menusInfo.SceneLoader.LoadLevelScene(LoadSceneMode.Additive);
            
            menusInfo.MainMenu.SetActive(false);
            menusInfo.GameplayMenu.SetActive(true);
        }
        public void OnExitButtonClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
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