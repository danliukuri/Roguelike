using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Pickers;
using UnityEngine;
using Zenject;

namespace Roguelike.Pickers
{
    public class KeyPicker : IPicker
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
        
        public bool TryToPickUp(Transform key)
        {
            bool isKeyPickedUp = dungeonInfo.KeysByRoom[entityMover.RoomIndex].Remove(key);
            if (isKeyPickedUp)
            {
                key.gameObject.SetActive(false);
                inventory.NumberOfKeys++;
            }
            return isKeyPickedUp;
        }
        #endregion
    }
}