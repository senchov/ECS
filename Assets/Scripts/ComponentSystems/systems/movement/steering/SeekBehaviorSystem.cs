using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SeekBehaviorSystem : JobComponentSystem
{
    private struct SeekBehaviorSystemJob : IJobProcessComponentData<SeekData, Position, VelocityData>
    {
        public void Execute([ReadOnly]ref SeekData seekData, [ReadOnly] ref Position pos, ref VelocityData velocity)
        {
            float2 sourcePos = new float2(pos.Value.x, pos.Value.y);
            velocity.Velocity += math.normalize(seekData.Target - sourcePos);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new SeekBehaviorSystemJob();
     //  job.Schedule ()
        return job.Schedule(this, inputDeps);
    }
}
