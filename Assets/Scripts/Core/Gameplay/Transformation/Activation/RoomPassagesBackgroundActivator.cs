using Roguelike.Core.Characteristics;
using Roguelike.Utilities.Generic;
using UnityEngine;

namespace Roguelike.Core.Gameplay.Transformation.Activation
{
    public class RoomPassagesBackgroundActivator : MonoBehaviour, IResettable
    {
        #region Fields
        [SerializeField] DirectionsArray<GameObject> passageBackgrounds;
        #endregion
        
        #region Methods
        public void Reset()
        {
            foreach (GameObject passageBackground in passageBackgrounds)
                passageBackground.SetActive(false);
        }
        
        public void Enable(int directionIndex) => passageBackgrounds[directionIndex].SetActive(true);
        public void ChangeOrientationToEast() => passageBackgrounds.ChangeOrientationToEast();
        #endregion
    }
}