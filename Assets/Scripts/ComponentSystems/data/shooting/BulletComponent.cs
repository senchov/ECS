using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct Bullet : IComponentData
{
    public float TimeToLive;
}

public class BulletComponent : ComponentDataWrapper<Bullet>
{
}
