using Roguelike.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
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

        public List<Transform> AllWallsMarkers => allWalls;
        public Transform[] AllDoorsMarkers => doors;
        public Transform[] AllExitsMarkers => exits;
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
        List<Transform> allWalls;
        #endregion

        #region Methods
        public void Initialize()
        {
            allPassages = new Transform[][]
            {
                passagesToTheNorth,
                passagesToTheWest,
                passagesToTheSouth,
                passagesToTheEast
            };

            allWalls = new List<Transform>(walls);
            for (int i = 0; i < allPassages.Length; i++)
                allWalls.AddRange(allPassages[i]);
        }

        public void SavePassagesDirectionsOnRotationToRight()
        {
            Transform[] tempPassagesToTheNorth = passagesToTheNorth;
            passagesToTheNorth = passagesToTheEast;
            passagesToTheEast = passagesToTheSouth;
            passagesToTheSouth = passagesToTheWest;
            passagesToTheWest = tempPassagesToTheNorth;

            passagesToTheWest = passagesToTheWest.Reverse().ToArray();
            passagesToTheEast = passagesToTheEast.Reverse().ToArray();
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