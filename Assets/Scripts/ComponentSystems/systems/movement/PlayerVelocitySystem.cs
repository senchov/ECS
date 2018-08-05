using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;

public class PlayerVelocitySystem : JobComponentSystem
{
    private struct PlayerVelocitySystemJob : IJobParallelFor
    {
        public ComponentDataArray<VelocityData> Velocities;
        public float3 ControllerPos;
        public float2 BigControllerPos;
        public float Speed;

        public void Execute(int index)
        {
            float2 controllerPos = new float2(ControllerPos.x, ControllerPos.y);

            VelocityData data = new VelocityData();
            data.Velocity = controllerPos - BigControllerPos;
            data.MaxSpeed = Speed;
            Velocities[index] = data;
        }
    }

    private struct PlayerGroup
    {
        public readonly int Length;
        public ComponentDataArray<PlayerData> PlayerTag;
        public ComponentDataArray<VelocityData> Velocity;
    }

    private struct SmallControllerGroup
    {
        public readonly int Length;
        public readonly ComponentDataArray<SmallController> SmallControllers;
        public readonly ComponentArray<Transform> Positions;
    }

    [Inject] PlayerGroup Player;
    [Inject] SmallControllerGroup Controller;
    [Inject] PlayerVelocitySystemBarrier Barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        PlayerVelocitySystemJob job = new PlayerVelocitySystemJob();
        job.Velocities = Player.Velocity;
        job.BigControllerPos = SetBigControllerPosition.Pos;
        job.ControllerPos = Controller.Positions[0].position;
        job.Speed = Player.PlayerTag[0].Speed;
        return job.Schedule(Player.Length, 64, inputDeps);
    }
}

public class PlayerVelocitySystemBarrier : BarrierSystem
{
}
