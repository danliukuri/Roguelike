namespace Roguelike.Core.Information
{
    public enum AnimatorParameter
    {
        #region Player
        SpawnTrigger,
        IsDespawning,
        IsMoving,
        IsDead,
        IsNoHeadShaking,
        IdleCycleOffset,
        #endregion
        
        #region TargetDetectionStatus
        IsDetected,
        #endregion
    }
}