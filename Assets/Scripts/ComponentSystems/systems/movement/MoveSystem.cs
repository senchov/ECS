using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class MoveSystem : JobComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<VelocityData> Velocities;
        public ComponentDataArray<Position> Positions;
    }

}
