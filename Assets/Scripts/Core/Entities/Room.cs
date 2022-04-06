using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Entities
{
    public class Room : MonoBehaviour
    {
        #region Properties
        public static Vector3[] Directions => new Vector3[]
        {
            Vector3.up,
            Vector3.left,
            Vector3.down,
            Vector3.right
        };
        public int Size => size;

        public List<Transform> AllPossibleWallsMarkers => allPossibleWalls;
        #endregion

        #region Fields
        [SerializeField] int size;
        [Header("Markers:")] 
        [SerializeField] Transform[] players;
        [SerializeField] Transform[] enemies;
        [SerializeField] Transform[] items;
        [SerializeField] Transform[] doors;
        [SerializeField] Transform[] exits;
        [Space]
        [SerializeField] Transform[] walls;
        [SerializeField] Transform[] passagesToTheNorth;
        [SerializeField] Transform[] passagesToTheWest;
        [SerializeField] Transform[] passagesToTheSouth;
        [SerializeField] Transform[] passagesToTheEast;

        Transform[][] allPassages;
        List<Transform> allPossibleWalls;
        #endregion

        #region Methods
        void Awake()
        {
            allPassages = new Transform[][]
            {
                passagesToTheNorth,
                passagesToTheWest,
                passagesToTheSouth,
                passagesToTheEast
            };

            allPossibleWalls = new List<Transform>(walls);
            for (int i = 0; i < allPassages.Length; i++)
                allPossibleWalls.AddRange(allPassages[i]);
        }

        public bool TryToCreatePassageTo(Room room)
        {
            bool isPassageCreated = false;

            for (int i = 0; i < allPassages.Length; i++)
            {
                const int NumberOfIterationsToOppositeDirection = 2;
                int oppositeDirectionIndex = (i + NumberOfIterationsToOppositeDirection) % allPassages.Length;

                if (room.transform.position == transform.position + Directions[i] * size &&
                        allPassages[i].Length != 0 && room.allPassages[oppositeDirectionIndex].Length != 0)
                {
                    OpenRandomPassage(new Transform[][]
                    {
                        allPassages[i],
                        room.allPassages[oppositeDirectionIndex]
                    });

                    isPassageCreated = true;
                }
            }

            return isPassageCreated;
        }
        void OpenRandomPassage(Transform[][] passagesMarkers)
        {
            int wallNumber = passagesMarkers[0].RandomIndex();
            List<Transform> wallsToOpen = new List<Transform>();

            for (int i = 0; i < passagesMarkers.Length; i++)
                wallsToOpen.Add(passagesMarkers[i][wallNumber]);

            foreach (Transform wallMarker in wallsToOpen)
                wallMarker.gameObject.SetActive(false);
        }
        #endregion
    }
}