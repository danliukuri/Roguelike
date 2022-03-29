using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Entities
{
    public class Room : MonoBehaviour
    {
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
        #endregion
    }
}