using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator))]
    public class IdleAnimationPlayer : MonoBehaviour
    {
        #region Fields
        [SerializeField] AnimationClip idle;
        Animator animator;
        #endregion
        
        #region Methods
        void Awake() => animator = GetComponent<Animator>();
        void OnEnable() => animator.Play(idle.name, default, Random.value);
        #endregion
    }
}