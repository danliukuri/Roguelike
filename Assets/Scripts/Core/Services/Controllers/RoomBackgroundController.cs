using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Core.Services.Controllers
{
    public class RoomBackgroundController : MonoBehaviour, IResettable
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

        public void EnablePassageBackground(int directionIndex) => passageBackgrounds[directionIndex].SetActive(true);
        public void ChangePassagesBackgroundOrientationToEast() => passageBackgrounds.ChangeOrientationToEast();
        #endregion
    }
}