using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public class BulletVelocitySystem : JobComponentSystem
{
    private struct BulletVelocitySetJob : IJobParallelFor
    {        
        public ComponentDataArray<VelocityData> Velocities;

        public void Execute(int index)
        {
            VelocityData data = new VelocityData();
            data.Velocity = new float2(1,1);
            Velocities[index] = data;
        }
    }

    private struct Data
    {
        public readonly int Length;        
        public ComponentDataArray<VelocityData> Velocities;
        public ComponentDataArray<Bullet> Bullets;
    }

    [Inject] Data BulletsData;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        BulletVelocitySetJob job = new BulletVelocitySetJob();
        job.Velocities = BulletsData.Velocities;
        return job.Schedule(BulletsData.Length,64,inputDeps);
    }

    //public Vector3 Seek(Vector2 sourcePos, Vector2 target)
    //{
    //    Vector2 desiredVelocity = (target - sourcePos).normalized * MaxSpeed;
    //    return desiredVelocity;
    //}
} 
