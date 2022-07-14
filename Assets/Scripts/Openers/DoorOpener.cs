using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Openers;
using UnityEngine;
using Zenject;

namespace Roguelike.Openers
{
    public class DoorOpener : IOpener
    {
        #region Fields
        DungeonInfo dungeonInfo;
        EntityMover entityMover;
        Inventory inventory;
        #endregion

        #region Methods
        [Inject]
        void Construct(DungeonInfo dungeonInfo, EntityMover entityMover, Inventory inventory)
        {
            this.dungeonInfo = dungeonInfo;
            this.entityMover = entityMover;
            this.inventory = inventory; 
        }
        
        public bool TryToOpen(Transform door)
        {
            bool canDoorBeOpened = false;
            if (inventory.NumberOfKeys > 0)
            {
                canDoorBeOpened = dungeonInfo.DoorsByRoom[entityMover.RoomIndex].Remove(door);
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