using System;
using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator))]
    public class PlayerTileAnimationChanger : MonoBehaviour
    {
        #region Fields
        [SerializeField] AnimatorParameter isMoving;
        [SerializeField] AnimatorParameter isNoHeadShaking;
        [SerializeField] AnimatorParameter isDespawning;
        [SerializeField] AnimatorParameter spawnTrigger;
        
        Animator animator;
        #endregion
        
        #region Methods
        void Awake() => animator = GetComponent<Animator>();
        
        public void ActivateSpawningAnimation() => spawnTrigger.Set();
        public void ActivateMovingAnimation() => isMoving.Set(true);
        public void DeactivateMovingAnimation() => isMoving.Set(false);
        public void ActivateNoHeadShakingAnimation() => isNoHeadShaking.Set(true);
        public void DeactivateNoHeadShakingAnimation() => isNoHeadShaking.Set(false);
        public void ActivateDespawningAnimation() => isDespawning.Set(true);
        public void DeactivateDespawningAnimation() => isDespawning.Set(false);
        
        public void DisableGameObject() => gameObject.SetActive(false);
        public void InvokeAfterAnimationFinished(Action playerTileAnimationChangerAction, int layerIndex = default) =>
            Invoke(playerTileAnimationChangerAction.Method.Name, GetCurrentAnimatorStateLength(layerIndex));
        float GetCurrentAnimatorStateLength(int layerIndex = default)
        {
            animator.Update(default);
            return animator.GetCurrentAnimatorStateInfo(layerIndex).length;
        }
        #endregion
    }
}