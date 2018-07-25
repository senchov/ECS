using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class InputConcurentSystem : JobComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Weapon> Weapons;       
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return base.OnUpdate(inputDeps);
    }
}
