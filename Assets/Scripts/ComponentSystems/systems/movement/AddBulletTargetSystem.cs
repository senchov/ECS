using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

public class AddBulletTargetSystem : JobComponentSystem
{
    private struct AddBulletTargetSystemJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray EntityArray;
        public EntityCommandBuffer.Concurrent EntityBuffer;
        public float2 Target;

        public void Execute(int index)
        {
            TargetPoint targetPoint = new TargetPoint();
            targetPoint.Value = Target;
            EntityBuffer.AddComponent(EntityArray[index], targetPoint);
        }
    }

    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Bullet> Bullets;
        public SubtractiveComponent<TargetPoint> Targets;
    }  

    private struct Group
    {
        public readonly int Length;
        public ComponentArray<InputData> Inputs;
    }

    [Inject] Data BulletData;
    [Inject] AddBulletSystemBarrier Barrier;
    [Inject] Group InpuData;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        AddBulletTargetSystemJob job = new AddBulletTargetSystemJob();       
        job.EntityArray = BulletData.Entities;
        job.EntityBuffer = Barrier.CreateCommandBuffer();
        job.Target = InpuData.Inputs[0].MousePos;
        return job.Schedule(BulletData.Length,64,inputDeps);
    }

}

public class AddBulletSystemBarrier : BarrierSystem
{
}
