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
        
        IRoomsPlacer roomsPlacer;
        #endregion

        #region Methods
        [Inject]
        public void Construct(IRoomsPlacer roomsPlacer)
        {
            this.roomsPlacer = roomsPlacer;
        }

        void Start()
        {
            roomsPlacer.Place(firstRoomPosition, startRoomsCount, transform);
        }
        #endregion
    }
}