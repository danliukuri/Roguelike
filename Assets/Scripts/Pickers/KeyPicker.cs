using Roguelike.Core.Information;
using Roguelike.Core.Pickers;
using UnityEngine;

namespace Roguelike.Pickers
{
    public class KeyPicker : IPicker
    {
        #region Fields
        readonly DungeonInfo dungeonInfo;
        readonly Inventory inventory;
        #endregion
        
        #region Methods
        public KeyPicker(DungeonInfo dungeonInfo, Inventory inventory)
        {
            this.dungeonInfo = dungeonInfo;
            this.inventory = inventory; 
        }
        
        public bool TryToPickUp(Transform key)
        {
            int keyRoomIndex = dungeonInfo.GetRoomIndex(key.position);
            bool isKeyPickedUp = dungeonInfo.KeysByRoom[keyRoomIndex].Remove(key);
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