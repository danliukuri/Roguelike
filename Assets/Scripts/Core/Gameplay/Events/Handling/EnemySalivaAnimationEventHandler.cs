using UnityEngine;

namespace Roguelike.Core.Gameplay.Events.Handling
{
    public class EnemySalivaAnimationEventHandler : MonoBehaviour
    {
        #region Methods
        public void OnSalivatingFinished() => gameObject.SetActive(false);
        #endregion
    }
}