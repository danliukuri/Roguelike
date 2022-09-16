using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorParameter : MonoBehaviour
    {
        #region Fields
        [SerializeField] string parameterName; 
        int parameterHash;
        Animator animator;
        #endregion
        
        #region Methods
        void Awake()
        {
            animator = GetComponent<Animator>();
            parameterHash = Animator.StringToHash(parameterName);
        }
        
        public void Set() => animator.SetTrigger(parameterHash);
        public void Set(bool value) => animator.SetBool(parameterHash, value);
        public void Set(float value) => animator.SetFloat(parameterHash, value);
        public void SetDefaultBool() => animator.SetBool(parameterHash, default);
        public void SetDefaultFloat() => animator.SetFloat(parameterHash, default);
        #endregion
    }
}