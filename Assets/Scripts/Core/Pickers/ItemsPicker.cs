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
        
        public void PickUpKey(Transform key)
        {
            key.gameObject.SetActive(false);
            dungeonInfo.KeysByRoom[dungeonInfo.PlayerRoomIndex].Remove(key);
            inventory.NumberOfKeys++;
        }
        #endregion
    }
}