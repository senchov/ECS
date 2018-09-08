using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class MoveSystem : JobComponentSystem
{

    private struct MoveSystemJob : IJobProcessComponentData<VelocityData, Position>
    {
        public float DeltaTime;

        public void Execute([ReadOnly]ref VelocityData velocity, ref Position position)
        {
            position.Value += new float3(velocity.Velocity.x, velocity.Velocity.y, 0) * DeltaTime * velocity.MaxSpeed;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new MoveSystemJob();
        job.DeltaTime = Time.deltaTime;
        return job.Schedule(this, inputDeps);
    }
}
