using System.Collections.Generic;
using System.Linq;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Core.Entities
{
    public class Room : MonoBehaviour, IResettable
    {
        #region Properties
        public static DirectionsArray<Vector3> NeighborDirections => new DirectionsArray<Vector3>
            { North = Vector3.up, West = Vector3.left, South = Vector3.down, East = Vector3.right };
        
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
        [SerializeField] Transform[] walls;
        [SerializeField] Transform[] players, enemies, items, doors, exits;
        [SerializeField] DirectionsArray<Transform[]> passages;
        
        List<Transform> allWalls;
        Vector2 upperSizeBounds, lowerSizeBounds;
        #endregion
        
        #region Methods
        private void Awake() => Initialize();
        void Initialize()
        {
            allWalls = new List<Transform>(walls);
            foreach (Transform[] passage in passages)
                allWalls.AddRange(passage);
        }
        public void Reset()
        {
            IEnumerable<GameObject> inactiveWallMarkers = allWalls
                .Select(wall => wall.gameObject)
                .Where(wallGameObject => !wallGameObject.activeSelf);
            
            foreach (GameObject inactiveWallMarker in inactiveWallMarkers)
                inactiveWallMarker.SetActive(true);
        }
        public void SetNewPositionAndSizeBounds(Vector3 newPosition)
        {
            Vector3 roomPosition = transform.position = newPosition;
            int halfSize = size / 2;
            upperSizeBounds = new Vector2(roomPosition.x + halfSize, roomPosition.y + halfSize);
            lowerSizeBounds = new Vector2(roomPosition.x - halfSize, roomPosition.y - halfSize);
        }
        
        public void RotateToRight() => ChangePassagesOrientationToEast();
        
        void ChangePassagesOrientationToEast()
        {
            passages.ChangeOrientationToEast();
            passages.West = passages.West.Reverse().ToArray();
            passages.East = passages.East.Reverse().ToArray();
        }
        void OpenRandomPassage(Transform[][] passagesMarkers)
        {
            int wallNumber = passagesMarkers.First().RandomIndex();
            List<Transform> wallsToOpen = new List<Transform>();
            
            for (int i = 0; i < passagesMarkers.Length; i++)
                wallsToOpen.Add(passagesMarkers[i][wallNumber]);
            
            foreach (Transform wallMarker in wallsToOpen)
                wallMarker.gameObject.SetActive(false);
        }
        public bool TryToCreatePassageTo(Room room)
        {
            bool isPassageCreated = false;
            foreach (Transform[] passage in passages)
            {
                int passageDirectionIndex = passages.DirectionIndexOf(passage);
                Vector3 neighborPosition = transform.position + NeighborDirections[passageDirectionIndex] * size;
                
                if (room.transform.position == neighborPosition)
                {
                    Transform[] neighborPassage =
                        room.passages[passages.OppositeDirectionIndexTo(passageDirectionIndex)];
                    OpenRandomPassage(new [] { passage, neighborPassage });
                    isPassageCreated = true;
                }
            }
            return isPassageCreated;
        }
        public bool IsInsideSizeBounds(Vector3 position)
        {
            return position.x < upperSizeBounds.x && position.x > lowerSizeBounds.x &&
                   position.y < upperSizeBounds.y && position.y > lowerSizeBounds.y;
        }
        #endregion
    }
}