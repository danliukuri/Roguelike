using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike
{
    public class GameObjectDeactivator : MonoBehaviour
    {
        #region Fields
        [SerializeField] float probabilityToBeDeactivatedOnEnable;
        #endregion
        
        #region Methods
        void OnEnable()
        {
            if(RandomExtensions.BoolValue(probabilityToBeDeactivatedOnEnable))
                Deactivate();
        }
        
        public void Deactivate() => gameObject.SetActive(false);
        #endregion
    }
}