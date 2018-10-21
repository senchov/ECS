using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class BulletDestroySystem : JobComponentSystem
{
    private struct BulletDestroyJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray EntityArray;
        public EntityCommandBuffer.Concurrent EntityBuffer;
        public float CurrentTime;
        public ComponentDataArray<Bullet> Bullets;

        public void Execute(int index)
        {
            if (CurrentTime > Bullets[index].RemoveAt)
                EntityBuffer.DestroyEntity(index,EntityArray [index]);            
        }
    }

    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Bullet> Bullets;
    }

    [Inject] Data BulletsData;
    [Inject] BulletDestroySystemBarrier Barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        BulletDestroyJob job = new BulletDestroyJob();
        job.EntityArray = BulletsData.Entities;
        job.EntityBuffer = Barrier.CreateCommandBuffer().ToConcurrent();
        job.Bullets = BulletsData.Bullets;
        job.CurrentTime = Time.time;
        return job.Schedule(BulletsData.Length,64,inputDeps);
    }

}

public class BulletDestroySystemBarrier : BarrierSystem
{
}
