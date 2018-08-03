using Unity.Entities;
using System;

[Serializable]
public struct SmallController : IComponentData
{
    public float MoveToMouseSpeed;
    public float ComeBackSpeed;
    public float MoveRadius;
    public float SmallControllerArriveSpeed;
    public float SmallControllerStopRadius;
}

public class SmallControllerComponent : ComponentDataWrapper<SmallController>
{
}
