using Unity.Jobs;
using Unity.Entities;
using UnityEngine;
using Unity.Collections;

public class ShootingSystem : JobComponentSystem
{
    private struct PlayerShootingJob : IJobParallelFor
    {
        [ReadOnly] public EntityArray EntityArray;
        public EntityCommandBuffer.Concurrent EntityBuffer;
        public float ReloadTime;

        public void Execute(int index)
        {
            EntityBuffer.AddComponent(EntityArray[index], new FireData()
            {
                ReloadAt = ReloadTime
            });
        }
    }

    private struct Data
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<Weapon> Weapons;
        public SubtractiveComponent<FireData> Firings;
    }
    
    [Inject] Data ShootData;
    [Inject] PlayerShootingBarrier Barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if (Input.GetButton("Fire2"))
        {
            return new PlayerShootingJob()
            {
                EntityArray = ShootData.Entities,
                EntityBuffer = Barrier.CreateCommandBuffer(),
                ReloadTime = Time.time + ShootData.Weapons[0].CooldownForReload
            }.Schedule(ShootData.Length, 64, inputDeps);
        }

        return base.OnUpdate(inputDeps);
    }
}

public class PlayerShootingBarrier : BarrierSystem
{
}
