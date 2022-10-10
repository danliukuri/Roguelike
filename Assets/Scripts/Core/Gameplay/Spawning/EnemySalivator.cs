using Roguelike.Core.Gameplay.Transformation.Rotation;
using Roguelike.Core.Information;
using Roguelike.Core.Setup.Factories;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Gameplay.Spawning
{
    [RequireComponent(typeof(SpriteRotator))]
    public class EnemySalivator : MonoBehaviour
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
        void OnDisable()
        {
            FinishSalivate();
            FinishSalivateMore();
        }
        
        public void StartSalivate() => InvokeRepeating(nameof(Salivate), Random.value, salivateRepeatRate);
        public void StartSalivateMore() => InvokeRepeating(nameof(SalivateMore), Random.value, salivateMoreRepeatRate);
        public void FinishSalivate() => CancelInvoke(nameof(Salivate));
        public void FinishSalivateMore() => CancelInvoke(nameof(SalivateMore));
        
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