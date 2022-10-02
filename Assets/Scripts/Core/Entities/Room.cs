using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Roguelike.Core.Entities
{
    public class Room : MonoBehaviour, IResettable
    {
        #region Properties
        public static DirectionsArray<Vector3> NeighborDirections => new DirectionsArray<Vector3>
            { North = Vector3.up, West = Vector3.left, South = Vector3.down, East = Vector3.right };
        
        public int Size => size;
        
        public Transform[] AllWallsMarkers { get; private set; }
        public Transform[] AllPlayerMarkers => players;
        public Transform[] AllEnemyMarkers => enemies;
        public Transform[] AllItemMarkers => items;
        public Transform[] AllExitMarkers => exits;
        public Transform[] AllDoorMarkers => doors;
        #endregion
        
        #region Fields
        public event Action<int> PassageOpened;
        public event Action RotatedToRight;
        
        [SerializeField] int size;
        [Header("Markers:")]
        [SerializeField] Transform[] walls;
        [SerializeField] Transform[] optionalWalls, players, enemies, items, doors, exits;
        [SerializeField] DirectionsArray<Transform[]> passages;
        [Header("Passage opening settings:")]
        [SerializeField, Min(default)] int minPassageWidth = DefaultMinPassageWidth;
        [SerializeField] float probabilityToOpenPassage = RandomExtensions.EqualProbability;
        
        const int DefaultMinPassageWidth = 1;
        Transform[] allMarkers;
        Dictionary<Vector3, List<Transform>> allMarkersByPosition;
        Vector2 upperSizeBounds, lowerSizeBounds;
        #endregion
        
        #region Methods
        void Awake() => Initialize();
        void Initialize()
        {
            AllWallsMarkers = walls.Union(optionalWalls).Union(passages.SelectMany(passages => passages)).ToArray();
            allMarkers = AllWallsMarkers.Union(players).Union(enemies).Union(items).Union(doors).Union(exits).ToArray();
        }
        public void Reset()
        {
            IEnumerable<GameObject> inactiveMarkers = allMarkers
                .Select(marker => marker.gameObject)
                .Where(markerGameObject => !markerGameObject.activeSelf);
            
            foreach (GameObject inactiveMarker in inactiveMarkers)
                inactiveMarker.SetActive(true);
        }
        
        public void SetNewPositionAndSizeBounds(Vector3 newPosition)
        {
            Vector3 roomPosition = transform.position = newPosition;
            int halfSize = size / 2;
            upperSizeBounds = new Vector2(roomPosition.x + halfSize, roomPosition.y + halfSize);
            lowerSizeBounds = new Vector2(roomPosition.x - halfSize, roomPosition.y - halfSize);
            
            SetAllMarkersByPosition();
        }
        void SetAllMarkersByPosition()
        {
            allMarkersByPosition = new Dictionary<Vector3, List<Transform>>(size * size);
            foreach (Transform marker in allMarkers)
                if (allMarkersByPosition.ContainsKey(marker.position))
                    allMarkersByPosition[marker.position].Add(marker);
                else
                    allMarkersByPosition.Add(marker.position, new List<Transform> { marker });
        }
        public void DeactivateEmptyMarkersOnPosition(Vector3 markerPosition)
        {
            if (allMarkersByPosition.ContainsKey(markerPosition))
                foreach (Transform marker in allMarkersByPosition[markerPosition])
                    if (marker.childCount == default)
                        marker.gameObject.SetActive(false);
        }
        
        public void RotateToRight()
        {
            ChangePassagesOrientationToEast();
            RotatedToRight?.Invoke();
        }
        void ChangePassagesOrientationToEast()
        {
            passages.ChangeOrientationToEast();
            passages.West = passages.West.Reverse().ToArray();
            passages.East = passages.East.Reverse().ToArray();
        }
        void OpenPassage(IReadOnlyList<Transform> passagesMarkers, int passageIndex, int passageWidth)
        {
            List<int> passagesToOpenIndexes = new List<int>();
            (int Left, int Right) passageNeighborsIndexes = (passageIndex - 1, passageIndex + 1);
            
            while (passagesToOpenIndexes.Count + 1 < passageWidth)
            {
                if (passageNeighborsIndexes.Left >= 0)
                    passagesToOpenIndexes.Add(passageNeighborsIndexes.Left--);
                else if(passageNeighborsIndexes.Right < passagesMarkers.Count)
                    passagesToOpenIndexes.Add(passageNeighborsIndexes.Right++);
            }
            
            if(passageWidth > 0)
                passagesMarkers[passageIndex].gameObject.SetActive(false);
            foreach (int passageMarkerIndex in passagesToOpenIndexes)
                if(RandomExtensions.BoolValue(probabilityToOpenPassage))
                    passagesMarkers[passageMarkerIndex].gameObject.SetActive(false);
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
                    
                    int randomPassageIndex = passage.RandomIndex();
                    OpenPassage(passage, randomPassageIndex, Random.Range(minPassageWidth, passage.Length));
                    OpenPassage(neighborPassage, randomPassageIndex, Random.Range(minPassageWidth, passage.Length));
                    
                    PassageOpened?.Invoke(passageDirectionIndex);
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