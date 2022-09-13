using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorParameter : MonoBehaviour
    {
        #region Fields
        [SerializeField] string parameterName;
        Animator animator;
        #endregion
        
        #region Methods
        void Awake() => animator = GetComponent<Animator>();
        
        public void Set(bool value) => animator.SetBool(parameterName, value);
        public void Set(float value) => animator.SetFloat(parameterName, value);
        public void SetDefaultBool() => animator.SetBool(parameterName, default);
        public void SetDefaultFloat() => animator.SetFloat(parameterName, default);
        #endregion
    }
}