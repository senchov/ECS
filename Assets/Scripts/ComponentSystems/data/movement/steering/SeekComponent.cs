using Unity.Entities;
using System;
using Unity.Mathematics;

[Serializable]
public struct SeekData : IComponentData
{
    public float2 Target;
}

public class SeekComponent : ComponentDataWrapper<SeekData>
{
}
