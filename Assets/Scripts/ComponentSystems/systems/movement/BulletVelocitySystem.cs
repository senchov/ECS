using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public class BulletVelocitySystem : JobComponentSystem
{
    private struct BulletVelocitySetJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray EntityArray;
        public EntityCommandBuffer.Concurrent EntityBuffer;
        public ComponentDataArray<VelocityData> Velocities;
        public ComponentDataArray<TargetPoint> Targets;
        public ComponentDataArray<Position> Positions;
        public float MaxSpeed;

        public void Execute(int index)
        {
            SetVelocity(index);
            EntityBuffer.AddComponent(EntityArray[index], new VelocitySetted());
        }

        private void SetVelocity(int index)
        {
            float2 position = new float2(Positions[index].Value.x, Positions[index].Value.y);
            float2 target = Targets[index].Value;

            VelocityData data = new VelocityData();
            data.Velocity = math.normalize(target - position);
            data.MaxSpeed = MaxSpeed;

            Velocities[index] = data;
        }
    }

    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Bullet> Bullets;
        public ComponentDataArray<TargetPoint> Targets;
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<VelocityData> Velocities;
        public SubtractiveComponent<VelocitySetted> SettedTag;
    }

    [Inject] Data BulletsData;
    [Inject] BulletVelocitySystemBarrier Barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        BulletVelocitySetJob job = new BulletVelocitySetJob();
        job.Velocities = BulletsData.Velocities;
        job.MaxSpeed = Bootstrap.MoveSettings.MoveBulletSpeed;
        job.Positions = BulletsData.Positions;
        job.Targets = BulletsData.Targets;
        job.EntityArray = BulletsData.Entities;
        job.EntityBuffer = Barrier.CreateCommandBuffer();

        return job.Schedule(BulletsData.Length, 64, inputDeps);
    }
}

public class BulletVelocitySystemBarrier : BarrierSystem
{
}


