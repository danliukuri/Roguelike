using System;
using Roguelike.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roguelike.Core.Entities
{
    public class Room : MonoBehaviour, IResettable
    {
        #region Properties
        public static Vector3[] Directions => new [] { Vector3.up, Vector3.left, Vector3.down, Vector3.right };

        public int Size => size;

        public List<Transform> AllWallsMarkers => allWalls;
        public Transform[] AllPlayerMarkers => players;
        public Transform[] AllEnemyMarkers => enemies;
        public Transform[] AllItemMarkers => items;
        public Transform[] AllExitMarkers => exits;
        public Transform[] AllDoorMarkers => doors;
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

        Vector2 upperSizeBounds;
        Vector2 lowerSizeBounds;
        #endregion

        #region Methods
        public void Initialize()
        {
            allPassages = new[] { passagesToTheNorth, passagesToTheWest, passagesToTheSouth, passagesToTheEast };
            
            allWalls = new List<Transform>(walls);
            for (int i = 0; i < allPassages.Length; i++)
                allWalls.AddRange(allPassages[i]);
        }
        public void SetNewPositionAndSizeBounds(Vector3 newPosition)
        {
            Vector3 roomPosition = transform.position = newPosition;
            int halfSize = size / 2;
            upperSizeBounds = new Vector2(roomPosition.x + halfSize, roomPosition.y + halfSize);
            lowerSizeBounds = new Vector2(roomPosition.x - halfSize, roomPosition.y - halfSize);
        }
        public void Reset()
        {
            IEnumerable<GameObject> inactiveWallMarkers = allWalls
                .Select(wall => wall.gameObject)
                .Where(wallGameObject => !wallGameObject.activeSelf);
            
            foreach (GameObject inactiveWallMarker in inactiveWallMarkers)
                inactiveWallMarker.SetActive(true);
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
        public bool IsInsideSizeBounds(Vector3 position)
        {
            return position.x < upperSizeBounds.x && position.x > lowerSizeBounds.x &&
                   position.y < upperSizeBounds.y && position.y > lowerSizeBounds.y;
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
                    OpenRandomPassage(new [] { allPassages[i], room.allPassages[oppositeDirectionIndex] });
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