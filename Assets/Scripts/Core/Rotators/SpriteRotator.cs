using System;
using UnityEngine;

namespace Roguelike.Core.Rotators
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRotator : MonoBehaviour
    {
        #region Fields
        public event Action RotatedLeft;
        public event Action RotatedRight;
        
        [SerializeField] bool isRotatedToRightByDefault;
        SpriteRenderer spriteRenderer;
        #endregion
        
        #region Methods
        void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
        
        public void RotateLeft()
        {
            RotatedLeft?.Invoke();
            spriteRenderer.flipX = isRotatedToRightByDefault;
        }
        public void RotateRight()
        {
            RotatedRight?.Invoke();
            spriteRenderer.flipX = !isRotatedToRightByDefault;
        }
        
        public bool IsLeftRotated() => isRotatedToRightByDefault ? spriteRenderer.flipX : !spriteRenderer.flipX;
        public bool TryToRotateRightOrLeft(Vector3 targetPosition) => TryToRotateRightOrLeft(
            () => targetPosition.x < transform.position.x, () => targetPosition.x > transform.position.x);
        public bool TryToRotateRightOrLeft(Func<bool> isRotateToLeft, Func<bool> isRotateToRight)
        {
            bool isRotated;
            if (isRotated = isRotateToLeft?.Invoke() ?? default)
                RotateLeft();
            else if (isRotated = isRotateToRight?.Invoke() ?? default)
                RotateRight();
            return isRotated;
        }
        #endregion
    }
}