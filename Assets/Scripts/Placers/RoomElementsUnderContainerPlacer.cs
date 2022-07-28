using Roguelike.Core.Factories;
using UnityEngine;

namespace Roguelike.Placers
{
    public class RoomElementsUnderContainerPlacer : RoomElementsPlacer
    {
        #region Fields
        readonly IObjectsContainerFactory containerFactory;
        readonly string containerName;
        #endregion

        #region Methods
        public RoomElementsUnderContainerPlacer(IGameObjectFactory gameObjectFactory, IObjectsContainerFactory containerFactory,
            string containerName) : base(gameObjectFactory)
        {
            this.containerFactory = containerFactory;
            this.containerName = containerName;
        }

        protected override void SetParent(Transform elementTransform, Transform defaultParent)
        {
            GameObject elementsContainer = containerFactory.GetContainer(containerName);
            elementsContainer.SetActive(true);
            
            Transform containerTransform = elementsContainer.transform;
            elementTransform.SetParent(containerTransform, false);
            
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