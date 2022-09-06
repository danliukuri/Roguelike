using UnityEngine;

namespace Roguelike.Core.EventHandlers
{
    public class EnemySalivaAnimationEventHandler : MonoBehaviour
    {
        #region Methods
        public void OnSalivatingFinished() => gameObject.SetActive(false);
        #endregion
    }
}