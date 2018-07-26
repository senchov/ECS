using Unity.Entities;
using System;
using Unity.Mathematics;

[Serializable]
public struct VelocityData : IComponentData
{
    public float2 Velocity;
    public float MaxSpeed;
}

public class VelocityComponent : ComponentDataWrapper<VelocityData>
{
}