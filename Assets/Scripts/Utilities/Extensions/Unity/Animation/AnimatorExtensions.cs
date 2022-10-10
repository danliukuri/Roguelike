using Roguelike.Animation.Information;
using UnityEngine;

namespace Roguelike.Utilities.Extensions.Unity.Animation
{
    public static class AnimatorExtensions
    {
        #region Methods
        public static void GetBool(this Animator animator, AnimatorParameter parameter) => 
            animator.GetBool(parameter.Id());
        public static void GetFloat(this Animator animator, AnimatorParameter parameter) =>
            animator.GetFloat(parameter.Id());
        public static void GetInteger(this Animator animator, AnimatorParameter parameter) =>
            animator.GetInteger(parameter.Id());
        
        public static void SetBool(this Animator animator, AnimatorParameter parameter, bool value) =>
            animator.SetBool(parameter.Id(), value);
        public static void SetFloat(this Animator animator, AnimatorParameter parameter, float value) =>
            animator.SetFloat(parameter.Id(), value);
        public static void SetInteger(this Animator animator, AnimatorParameter parameter, int value) =>
            animator.SetInteger(parameter.Id(), value);
        
        public static void SetTrigger(this Animator animator, AnimatorParameter parameter) =>
            animator.SetTrigger(parameter.Id());
        public static void ResetTrigger(this Animator animator, AnimatorParameter parameter) =>
            animator.ResetTrigger(parameter.Id());
        
        public static float GetCurrentAnimatorStateLength(this Animator animator, int layerIndex = default)
        {
            animator.Update(default);
            return animator.GetCurrentAnimatorStateInfo(layerIndex).length;
        }
        #endregion
    }
}