using Roguelike.Core.Setup.Factories;
using Roguelike.Core.Setup.Placing;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Loading
{
    public class GameObjectLoader : MonoBehaviour
    {
        #region Fields
        IGameObjectFactory objectFactory;
        IGameObjectPlacer placer;
        GameObject placedObject;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(IGameObjectFactory objectFactory, IGameObjectPlacer placer)
        {
            this.objectFactory = objectFactory;
            this.placer = placer;
        }
        
        void OnEnable()
        {
            if (placedObject != default)
                placer.Place(placedObject).SetActive(true);
        }
        void Start() => placer.Place(placedObject = objectFactory.GetGameObject()).SetActive(true);
        #endregion
    }
}