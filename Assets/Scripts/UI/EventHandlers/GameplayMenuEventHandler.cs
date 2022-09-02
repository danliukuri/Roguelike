using TMPro;

namespace Roguelike.UI.EventHandlers
{
    public class GameplayMenuEventHandler
    {
        #region Fields
        readonly TMP_Text levelNumberTMP;
        #endregion
        
        #region Methods
        public GameplayMenuEventHandler(TMP_Text levelNumberTMP) => this.levelNumberTMP = levelNumberTMP;
        public void OnLevelNumberChanged(int levelNumber) => levelNumberTMP.text = levelNumber.ToString();
        #endregion
    }
}