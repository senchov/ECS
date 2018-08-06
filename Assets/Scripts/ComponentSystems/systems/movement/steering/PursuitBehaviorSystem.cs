using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public class PursuitBehaviorSystem : JobComponentSystem
{
    private struct PursuitBehaviorSystemJob : IJobProcessComponentData<PursuitData, Position, VelocityData>
    {
        public void Execute([ReadOnly]ref PursuitData pursuitData, [ReadOnly] ref Position pos, ref VelocityData velocity)
        {
            float2 sourcePos = new float2(pos.Value.x, pos.Value.y);           
            Steering steer = new Steering();
            steer.MaxSpeed = pursuitData.ArriveSpeed;
            float2 futureTargetPos =  pursuitData.Target;
            float2 desireVelocity = steer.Seek(sourcePos, futureTargetPos)- velocity.Velocity; /*steer.Arrive(sourcePos, futureTargetPos, pursuitData.StopRadius);*/
            velocity.Velocity += desireVelocity;
        }
        
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new PursuitBehaviorSystemJob();
        return job.Schedule(this, 64, inputDeps);
    }
}
