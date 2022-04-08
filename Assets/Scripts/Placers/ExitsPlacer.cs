using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roguelike.Placers
{
    public class ExitsPlacer : IExitsPlacer
    {
        #region Fields
        readonly IExitFactory exitFactory;
        #endregion

        #region Methods
        public ExitsPlacer(IExitFactory exitFactory) => this.exitFactory = exitFactory;
        public void Place(List<Transform> exitsMarkers)
        {
            Transform exitMarker = exitsMarkers.Where(m => m.gameObject.activeSelf).Random();

            GameObject exitGameObject = exitFactory.GetExit();
            Transform exitTransform = exitGameObject.transform;

            exitTransform.position = exitMarker.position;
            exitTransform.SetParent(exitMarker);

            exitGameObject.SetActive(true);
        }
        #endregion
    }
}
