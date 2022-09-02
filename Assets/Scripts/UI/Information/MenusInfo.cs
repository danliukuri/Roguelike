using Roguelike.Loaders;
using UnityEngine;

namespace Roguelike.UI.Information
{
    public class MenusInfo
    {
        #region Properties
        public GameObject MainMenu { get; }
        public GameObject PlayerDeathMenu { get; }
        public GameObject GameplayMenu { get; }
        
        public SceneLoader SceneLoader { get; }
        #endregion
        
        #region Methods
        public MenusInfo(GameObject mainMenu, GameObject playerDeathMenu, GameObject gameplayMenu,
            SceneLoader sceneLoader)
        {
            MainMenu = mainMenu;
            PlayerDeathMenu = playerDeathMenu;
            GameplayMenu = gameplayMenu;
            
            SceneLoader = sceneLoader;
        }
        #endregion
    }
}