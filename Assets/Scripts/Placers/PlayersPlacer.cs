using Roguelike.Core.Factories;
using UnityEngine;

namespace Roguelike.Placers
{
    public class PlayersPlacer : RoomElementsPlacer
    {
        #region Fields
        const string playerParentName = "Players";
        readonly IObjectsContainerFactory containerFactory;
        #endregion

        #region Methods
        public PlayersPlacer(IGameObjectFactory gameObjectFactory, IObjectsContainerFactory containerFactory) :
            base(gameObjectFactory) => this.containerFactory = containerFactory;
        protected override void SetParent(Transform playerTransform, Transform parent)
        {
            GameObject playersContainer = containerFactory.GetContainer(playerParentName);
            playerTransform.SetParent(playersContainer.transform, false);
            playersContainer.SetActive(true);
        }
        #endregion
    }
}