using TMPro;

namespace Roguelike.UI.EventHandlers
{
    public class GameplayMenuEventHandler
    {
        #region Fields
        readonly TMP_Text levelNumberTMP;
        readonly TMP_Text keysNumberTMP;
        #endregion
        
        #region Methods
        public GameplayMenuEventHandler(TMP_Text levelNumberTMP, TMP_Text keysNumberTMP)
        {
            this.levelNumberTMP = levelNumberTMP;
            this.keysNumberTMP = keysNumberTMP;
        }
        public void OnLevelNumberChanged(int levelNumber) => levelNumberTMP.text = levelNumber.ToString();
        public void OnPlayerNumberOfKeysChanged(int numberOfKeys) => keysNumberTMP.text = numberOfKeys.ToString();
        #endregion
    }
}