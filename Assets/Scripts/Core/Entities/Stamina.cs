using Roguelike.Gameplay.Tracking.Finishing;

namespace Roguelike.Core.Entities
{
    public class Stamina
    {
        #region Properties
        public TurnFinisher TurnFinisher { get; set; }
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
        public Stamina(int numberOfActionsToUseStamina, int numberOfActionsForRestoringStamina)
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
                TurnFinisher.NumberOfFreeActionsPerTurn++;
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
                TurnFinisher.NumberOfFreeActionsPerTurn--;
            }
            return isStaminaRestored;
        }
        #endregion
    }
}