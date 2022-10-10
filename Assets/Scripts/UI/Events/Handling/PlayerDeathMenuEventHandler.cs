using Roguelike.Core.Information;
using Roguelike.UI.Information;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Roguelike.UI.Events.Handling
{
    public class PlayerDeathMenuEventHandler : MonoBehaviour
    {
        #region Fields
        MenusInfo menusInfo;
        #endregion
        
        #region Methods
        [Inject] 
        void Construct(MenusInfo menusInfo) => this.menusInfo = menusInfo;
        
        public void OnTryAgainButtonClick()
        {
            menusInfo.SceneLoader.UnloadLevelScene();
            menusInfo.SceneLoader.LoadLevelScene(LoadSceneMode.Additive);
            
            menusInfo.PlayerDeathMenu.SetActive(false);
        }
        public void OnMainMenuButtonClick()
        {
            menusInfo.SceneLoader.UnloadLevelScene();
            
            menusInfo.PlayerDeathMenu.SetActive(false);
            menusInfo.GameplayMenu.SetActive(false);
            menusInfo.MainMenu.SetActive(true);
        }
        public void OnPlayerDeath(object sender, MovingEventArgs e) => menusInfo.PlayerDeathMenu.SetActive(true);
        #endregion
    }
}