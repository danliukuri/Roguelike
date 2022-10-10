using Roguelike.Core.Gameplay.Interaction.Openers;
using Roguelike.Core.Information;
using UnityEngine;

namespace Roguelike.Gameplay.Interaction.Opening
{
    public class DoorOpener : IOpener
    {
        #region Fields
        readonly DungeonInfo dungeonInfo;
        readonly Inventory inventory;
        #endregion
        
        #region Methods
        public DoorOpener(DungeonInfo dungeonInfo, Inventory inventory)
        {
            this.dungeonInfo = dungeonInfo;
            this.inventory = inventory; 
        }
        
        public bool TryToOpen(Transform door)
        {
            bool canDoorBeOpened = false;
            if (inventory.NumberOfKeys > 0)
            {
                int doorRoomIndex = dungeonInfo.GetRoomIndex(door.position);
                canDoorBeOpened = dungeonInfo.DoorsByRoom[doorRoomIndex].Remove(door);
                if (canDoorBeOpened)
                {
                    door.gameObject.SetActive(false);
                    inventory.NumberOfKeys--;  
                }
            }
            return canDoorBeOpened;
        }
        #endregion
    }
}