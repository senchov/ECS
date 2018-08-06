using Unity.Entities;
using System;
using Unity.Mathematics;

[Serializable]
public struct PursuitData : IComponentData
{
    public float2 Target;
    public float2 TargetVelocity;   
    public float ArriveSpeed;
    public float StopRadius;
}


public class PursuitBehaviorComponent : ComponentDataWrapper<PursuitData>
{
}
