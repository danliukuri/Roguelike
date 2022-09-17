using System;
using System.Collections.Generic;
using Roguelike.Core.Information;
using UnityEngine;

namespace Roguelike.Utilities.Extensions
{
    public static class AnimatorParameterExtensions
    {
        #region Fields
        static readonly Dictionary<AnimatorParameter, int> AnimatorParameterIds;
        #endregion
        
        #region Methods
        static AnimatorParameterExtensions()
        {
            Array animatorParameters = Enum.GetValues(typeof(AnimatorParameter));
            
            AnimatorParameterIds = new Dictionary<AnimatorParameter, int>(animatorParameters.Length);
            foreach (AnimatorParameter parameter in animatorParameters)
                AnimatorParameterIds.Add(parameter, Animator.StringToHash(parameter.ToString()));
        }
        
        public static int Id(this AnimatorParameter parameter) => AnimatorParameterIds[parameter];
        #endregion
    }
}