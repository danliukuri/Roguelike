using Roguelike.Finishers;

namespace Roguelike.Core.Services.Managers
{
    public class StaminaManager
    {
        #region Properties
        public TurnFinisher turnFinisher { get; set; }
        #endregion
        
        #region Fields
        const int NumberOfActionsToStartRestoringStamina = 0;
        
        readonly int numberOfActionsToUseStamina;
        readonly int numberOfActionsForRestoringStamina;
        int staminaPoints;
        
        bool isNeededToUseStamina = true;
        bool isNeededToRestoreStamina;
        #endregion
        
        #region Methods
        public StaminaManager(int numberOfActionsToUseStamina, int numberOfActionsForRestoringStamina)
        {
            this.numberOfActionsToUseStamina = staminaPoints = numberOfActionsToUseStamina;
            this.numberOfActionsForRestoringStamina = numberOfActionsForRestoringStamina;
        }
        
        public bool TryToUse()
        {
            bool canUse = isNeededToUseStamina;
            if (canUse)
            {
                staminaPoints--;
                TryToStartRestoringStamina();
            }
            return canUse;
        }
        public bool TryToRestore()
        {
            bool canRestore = isNeededToRestoreStamina;
            if (canRestore)
            {
                staminaPoints++;
                TryToStartUsingStamina();
            }
            return canRestore;
        }
        
        bool TryToStartRestoringStamina()
        {
            bool isStaminaGone = staminaPoints <= NumberOfActionsToStartRestoringStamina;
            if (isStaminaGone)
            {
                isNeededToRestoreStamina = true;
                isNeededToUseStamina = false;
                turnFinisher.NumberOfFreeActionsPerTurn++;
            }
            return isStaminaGone;
        }
        bool TryToStartUsingStamina()
        {
            bool isStaminaRestored = staminaPoints >= numberOfActionsForRestoringStamina;
            if (isStaminaRestored)
            {
                isNeededToRestoreStamina = false;
                isNeededToUseStamina = true;
                staminaPoints = numberOfActionsToUseStamina;
                turnFinisher.NumberOfFreeActionsPerTurn--;
            }
            return isStaminaRestored;
        }
        #endregion
    }
}