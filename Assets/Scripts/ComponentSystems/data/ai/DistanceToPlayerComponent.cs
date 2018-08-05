using Unity.Entities;
using System;

[Serializable]
public struct DistanceToPlayer : IComponentData
{
    public float Distance;
}

public class DistanceToPlayerComponent : ComponentDataWrapper<DistanceToPlayer>
{
}
