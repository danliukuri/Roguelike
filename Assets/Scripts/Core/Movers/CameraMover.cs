using System.Collections;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Movers
{
    public class CameraMover : MonoBehaviour
    {
        #region Fields
        [SerializeField] Vector3 positionOffset;
        [SerializeField] float movingBetweenRoomsDuration;
        
        Coroutine smoothMoving;
        DungeonInfo dungeonInfo;
        EntityMover playerMover;
        #endregion

        #region Methods

        [Inject]
        void Construct(DungeonInfo dungeonInfo, EntityMover playerMover)
        {
            this.dungeonInfo = dungeonInfo;
            this.playerMover = playerMover;
        }
        
        void OnEnable() => playerMover.RoomIndexChanged += MoveToRoom;
        void OnDisable() => playerMover.RoomIndexChanged -= MoveToRoom;
        
        void MoveToRoom(int roomIndex)
        {
            if(smoothMoving != default)
                StopCoroutine(smoothMoving);
            
            Vector3 targetPosition = dungeonInfo.Rooms[roomIndex].transform.position + positionOffset;
            smoothMoving = StartCoroutine(SmoothMoving(transform.position, targetPosition));
        }
        IEnumerator SmoothMoving(Vector3 initialPosition, Vector3 targetPosition)
        {   
            float elapsedTime = default;
            while (transform.position != targetPosition)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / movingBetweenRoomsDuration;

                const float interpolationStartValue = 0f, interpolationEndValue = 1f;
                float interpolationValue = 
                    Mathf.SmoothStep(interpolationStartValue, interpolationEndValue, percentageComplete);

                transform.position = Vector3.Lerp(initialPosition, targetPosition, interpolationValue);
                yield return default;
            }
        }
        #endregion
    }
}