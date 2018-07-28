using Unity.Entities;
using System;
using Unity.Mathematics;

[Serializable]
public struct BigController : IComponentData
{
    public float2 Offset;
}

public class BigControllerComponent : ComponentDataWrapper<BigController>
{
}
