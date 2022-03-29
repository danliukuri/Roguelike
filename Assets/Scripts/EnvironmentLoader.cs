using Roguelike.Core.Placers;
using UnityEngine;
using Zenject;

namespace Roguelike
{
    public class EnvironmentLoader : MonoBehaviour
    {
        #region Fields
        [SerializeField] Vector3 firstRoomPosition;
        [SerializeField] int startRoomsCount;

        IDungeonPlacer dungeonPlacer;
        #endregion

        #region Methods
        [Inject]
        public void Construct(IDungeonPlacer dungeonPlacer) => this.dungeonPlacer = dungeonPlacer;

        void Start()
        {
            dungeonPlacer.Place(firstRoomPosition, startRoomsCount, transform);
        }
        #endregion
    }
}