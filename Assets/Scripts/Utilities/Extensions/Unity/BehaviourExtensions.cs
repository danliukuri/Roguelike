using System;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Utilities.Extensions.Unity.Animation;
using UnityEngine;

namespace Roguelike.Utilities.Extensions.Unity
{
    public static class BehaviourExtensions
    {
        #region MonoBehaviour
        #region Fields
        static readonly Dictionary<MonoBehaviour ,Dictionary<Action, Coroutine>> Coroutines =
            new Dictionary<MonoBehaviour, Dictionary<Action, Coroutine>>();
        #endregion
        
        #region Methods
        static IEnumerator InvokeRoutine(this MonoBehaviour monoBehaviour, Action action, YieldInstruction instruction)
        {
            yield return instruction;
            action.Invoke();
            Coroutines[monoBehaviour][action] = default;
        }
        
        public static void StartInvokeCoroutine(this MonoBehaviour monoBehaviour, Action action,
            YieldInstruction instruction = default)
        {
            if (action != default)
            {
                Coroutine coroutine = monoBehaviour.StartCoroutine(monoBehaviour.InvokeRoutine(action, instruction));
                
                if (!Coroutines.ContainsKey(monoBehaviour))
                    Coroutines.Add(monoBehaviour, new Dictionary<Action, Coroutine>());
                
                if (Coroutines[monoBehaviour].ContainsKey(action))
                    Coroutines[monoBehaviour][action] = coroutine;
                else
                    Coroutines[monoBehaviour].Add(action, coroutine); 
            }
        }
        public static void StartInvokeCoroutine(this MonoBehaviour monoBehaviour, Action action,
            float secondsToInvoke) => monoBehaviour.StartInvokeCoroutine(action, new WaitForSeconds(secondsToInvoke));
        public static void StartInvokeCoroutineAfterCurrentAnimationFinished(this MonoBehaviour monoBehaviour,
            Action action, Animator animator, int layerIndex = default) =>
            monoBehaviour.StartInvokeCoroutine(action, animator.GetCurrentAnimatorStateLength(layerIndex));
        
        public static void StopInvokeCoroutine(this MonoBehaviour monoBehaviour, Action action)
        {
            if (Coroutines.ContainsKey(monoBehaviour) && Coroutines[monoBehaviour].ContainsKey(action))
            {
                monoBehaviour.StopCoroutine(Coroutines[monoBehaviour][action]);
                Coroutines[monoBehaviour][action] = default;
            }
        }
        public static bool IsInvokeCoroutineRunning(this MonoBehaviour monoBehaviour, Action action) =>
            Coroutines.ContainsKey(monoBehaviour) && Coroutines[monoBehaviour].ContainsKey(action) &&
            Coroutines[monoBehaviour][action] != default;
        #endregion
        #endregion
    }
}