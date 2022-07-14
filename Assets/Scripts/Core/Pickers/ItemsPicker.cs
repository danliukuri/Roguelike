using Roguelike.Core.Entities;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Pickers
{
    public class ItemsPicker
    {
        #region Fields
        DungeonInfo dungeonInfo;
        Inventory inventory;
        #endregion

        #region Methods
        [Inject]
        void Construct(DungeonInfo dungeonInfo, Inventory inventory)
        {
            this.dungeonInfo = dungeonInfo;
            this.inventory = inventory; 
        }
        
        public bool TryToPickUpKey(Transform key)
        {
            bool isKeyPickedUp = dungeonInfo.KeysByRoom[dungeonInfo.PlayerRoomIndex].Remove(key);
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