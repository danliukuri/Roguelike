using Roguelike.Core.Factories;
using Roguelike.Core.Rotators;
using UnityEngine;
using Zenject;

namespace Roguelike.Core
{
    [RequireComponent(typeof(SpriteRotator))]
    public class EnemyMouth : MonoBehaviour
    {
        #region Fields
        [SerializeField] float salivateRepeatRate;
        
        IGameObjectFactory salivaFactory;
        SpriteRotator rotator;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(IGameObjectFactory salivaFactory) => this.salivaFactory = salivaFactory;
        void Awake() => rotator = GetComponent<SpriteRotator>();
        void OnEnable() => InvokeRepeating(nameof(Salivate), Random.value, salivateRepeatRate);
        void OnDisable() => CancelInvoke(nameof(Salivate));
        
        public void Salivate()
        {
            GameObject saliva = salivaFactory.GetGameObject();
            saliva.transform.position = transform.position;
            
            if(rotator.IsLeftRotated())
                saliva.GetComponent<SpriteRotator>().RotateLeft();
            else
                saliva.GetComponent<SpriteRotator>().RotateRight();
            
            saliva.SetActive(true);
        }
        #endregion
    }
}