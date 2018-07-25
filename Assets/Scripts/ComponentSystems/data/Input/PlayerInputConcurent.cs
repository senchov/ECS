using System;
using Unity.Entities;

[Serializable]
public struct PlayerInput : IComponentData
{
    public int Fire1;
}

public class PlayerInputConcurent : ComponentDataWrapper<PlayerInput>
{
}
