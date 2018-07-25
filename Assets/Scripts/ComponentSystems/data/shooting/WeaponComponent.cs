using System;
using Unity.Entities;

[Serializable]
public struct Weapon : IComponentData
{
    public float CooldownForReload;
}

public class WeaponComponent : ComponentDataWrapper<Weapon>
{
}
