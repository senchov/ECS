using Unity.Entities;
using System;

[Serializable]
public struct PlayerData: IComponentData
{
    public float Speed;
}

public class PlayerTag : ComponentDataWrapper<PlayerData>
{
}
