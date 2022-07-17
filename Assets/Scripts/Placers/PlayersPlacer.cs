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
        protected override void SetParent(Transform playerTransform, Transform defaultParent)
        {
            GameObject playersContainer = containerFactory.GetContainer(playerParentName);
            playersContainer.SetActive(true);
            
            Transform containerTransform = playersContainer.transform;
            playerTransform.SetParent(containerTransform, false);
            
            MoveContainerToTopLevelOfObjectScene(defaultParent);
            void MoveContainerToTopLevelOfObjectScene(Transform objectTransform)
            {
                containerTransform.SetParent(objectTransform);
                containerTransform.SetParent(default);
            }
        }
        #endregion
    }
}