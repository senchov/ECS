using Unity.Entities;
using System;

[Serializable]
public struct Bullet : IComponentData
{
    public float RemoveAt;
}

public class BulletComponent : ComponentDataWrapper<Bullet>
{
}
