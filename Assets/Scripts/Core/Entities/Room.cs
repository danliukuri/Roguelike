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
        #endregion

        #region Fields
        [SerializeField] int size;
        [Header("Markers:")]
        [SerializeField] List<Transform> doors;
        [SerializeField] List<Transform> items;
        [SerializeField] List<Transform> enemies;
        [SerializeField] List<Transform> walls;
        [Space]
        [SerializeField] List<Transform> passagesToTheNorth;
        [SerializeField] List<Transform> passagesToTheWest;
        [SerializeField] List<Transform> passagesToTheSouth;
        [SerializeField] List<Transform> passagesToTheEast;

        List<Transform>[] allPassages;
        #endregion

        #region Methods
        void Awake()
        {
            allPassages = new List<Transform>[]
            {
                passagesToTheNorth,
                passagesToTheWest,
                passagesToTheSouth,
                passagesToTheEast
            };
        }

        public bool TryToCreatePassageTo(Room room)
        {
            bool isPassageCreated = false;

            for (int i = 0; i < allPassages.Length; i++)
            {
                const int NumberOfIterationsToOppositeDirection = 2;
                int oppositeDirectionIndex = (i + NumberOfIterationsToOppositeDirection) % allPassages.Length;

                if (room.transform.position == transform.position + Directions[i] * size &&
                        allPassages[i].Count != 0 && room.allPassages[oppositeDirectionIndex].Count != 0)
                {
                    OpenRandomPassage(new List<Transform>[]
                    {
                        allPassages[i],
                        room.allPassages[oppositeDirectionIndex]
                    });

                    isPassageCreated = true;
                }
            }

            return isPassageCreated;
        }
        void OpenRandomPassage(List<Transform>[] passagesMarkers)
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