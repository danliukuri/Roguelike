using Roguelike.Core.Entities;
using Roguelike.Core.Placers;
using System.Collections.Generic;
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
            List<Room> rooms = roomsPlacer.Place(firstRoomPosition, startRoomsCount, transform);
            CreatePassageBetweenRooms(rooms);
        }

        void CreatePassageBetweenRooms(List<Room> rooms)
        {
            for (int i = 0; i < rooms?.Count; i++)
                for (int j = i; j < rooms.Count; j++)
                    rooms[i].TryToCreatePassageTo(rooms[j]);
        }
        #endregion
    }
}