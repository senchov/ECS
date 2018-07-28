using Unity.Entities;
using System;

[Serializable]
public struct SmallController : IComponentData
{
    public float ComebackSpeed;
    public float MoveRadius;
}

public class SmallControllerComponent : ComponentDataWrapper<SmallController>
{
}
