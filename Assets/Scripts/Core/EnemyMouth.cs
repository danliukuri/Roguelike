using Roguelike.Core.Factories;
using Roguelike.Core.Information;
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
        [SerializeField] float salivateMoreRepeatRate;
        
        IGameObjectFactory salivaFactory;
        IGameObjectFactory bigSalivaFactory;
        SpriteRotator rotator;
        #endregion
        
        #region Methods
        [Inject]
        void Construct([Inject(Id = EntityType.Saliva)] IGameObjectFactory salivaFactory,
            [Inject(Id = EntityType.BigSaliva)] IGameObjectFactory bigSalivaFactory)
        {
            this.salivaFactory = salivaFactory;
            this.bigSalivaFactory = bigSalivaFactory;
        }
        
        void Awake() => rotator = GetComponent<SpriteRotator>();
        void OnEnable() => InvokeRepeating(nameof(Salivate), Random.value, salivateRepeatRate);
        void OnDisable() => CancelInvoke(nameof(Salivate));
        
        public void StartSalivateMore()
        {
            CancelInvoke(nameof(Salivate));
            InvokeRepeating(nameof(SalivateMore), Random.value, salivateMoreRepeatRate);
        }
        public void FinishSalivateMore()
        {
            CancelInvoke(nameof(SalivateMore));
            InvokeRepeating(nameof(Salivate), Random.value, salivateRepeatRate);
        }
        
        public void Salivate() => Salivate(salivaFactory.GetGameObject());
        public void SalivateMore() => Salivate(bigSalivaFactory.GetGameObject());
        void Salivate(GameObject saliva)
        {
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