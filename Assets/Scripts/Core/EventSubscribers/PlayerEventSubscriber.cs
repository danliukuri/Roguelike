using Roguelike.Core.Information;
using Roguelike.Core.Pickers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class PlayerEventSubscriber : MonoBehaviour
    {
        #region Fields
        ItemsPicker itemsPicker;

        DungeonInfo dungeonInfo;
        #endregion

        #region Methods
        [Inject]
        void Construct(DungeonInfo dungeonInfo, ItemsPicker itemsPicker)
        {
            this.dungeonInfo = dungeonInfo;
            this.itemsPicker = itemsPicker;
        }

        void OnEnable()
        {
            dungeonInfo.PlayerToKeyMoving += itemsPicker.PickUpKey;
        }
        void OnDisable()
        {
            dungeonInfo.PlayerToKeyMoving -= itemsPicker.PickUpKey;
        }
        #endregion
    }
}