using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class ReloadSystem : JobComponentSystem
{
    private struct ReloadJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray EntityArray;
        public EntityCommandBuffer.Concurrent EntityBuffer;
        public float CurrentTime;
        public ComponentDataArray<FireData> Firings;

        public void Execute(int index)
        {
            if (CurrentTime > Firings[index].ReloadAt)
                EntityBuffer.RemoveComponent<FireData>(index, EntityArray[index]);
        }
    }

    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<FireData> Firings;
    }

    [Inject] Data ShootData;
    [Inject] PlayerReloadBarrier Barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new ReloadJob()
        {
            EntityArray = ShootData.Entities,
            EntityBuffer = Barrier.CreateCommandBuffer().ToConcurrent(),
            CurrentTime = Time.time,
            Firings = ShootData.Firings
        }.Schedule(ShootData.Length, 64, inputDeps);
    }

}

public class PlayerReloadBarrier : BarrierSystem
{
}
