using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SmallComponentVelocitySystem : JobComponentSystem
{
    [BurstCompile]
    private struct SmallComponentVelocityJob : IJobParallelFor
    {
        public ComponentDataArray<VelocityData> Velocities;
        public ComponentDataArray<Position> SmallControllerPos;
        public float2 BigControllerPos;

        public float MaxSpeed;
        public float MaxArriveSpeed;
        public float SlowingRadius;

        public void Execute(int index)
        {
            Steering steering = new Steering();
            steering.MaxSpeed = MaxArriveSpeed;
            float2 sourcePos = new float2(SmallControllerPos[index].Value.x, SmallControllerPos[index].Value.y);
            float2 sourceVel = Velocities[index].Velocity;
            float2 targetPos = BigControllerPos;

            VelocityData data = new VelocityData();
            data.Velocity = steering.Arrive(sourcePos, sourceVel, targetPos, SlowingRadius);
            data.MaxSpeed = MaxSpeed;

            Velocities[index] = data;
        }
    }

    private struct Group
    {
        public readonly int Length;
        public ComponentArray<InputData> Inputs;
    }

    private struct BigControllerGroup
    {
        public readonly int Length;
        [ReadOnly] public ComponentDataArray<BigController> BigControllers;
        [ReadOnly] public ComponentDataArray<Position> Positions;
    }

    private struct SmallControllerGroup
    {
        public readonly int Length;
        public ComponentDataArray<SmallController> SmallControllers;
        public ComponentDataArray<VelocityData> Velocities;
        public ComponentDataArray<Position> Positions;
    }

    [Inject] Group InputDataInjected;
    [Inject] BigControllerGroup BigController;
    [Inject] SmallControllerGroup SmallController;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle handle = base.OnUpdate(inputDeps);
        InputData input = InputDataInjected.Inputs[0];
        //if (input.IsMouse0Pressed)
        //{
        float2 bigControllerPos = SetBigControllerPosition.Pos;

        //if ( math.distance(input.MousePos, bigControllerPos) < SmallController.SmallControllers[0].MoveRadius)
        //{
        SmallComponentVelocityJob job = new SmallComponentVelocityJob();
        job.BigControllerPos = bigControllerPos;

        job.MaxSpeed = Bootstrap.MoveSettings.SmallControllerForwardSpeed;
        job.MaxArriveSpeed = Bootstrap.MoveSettings.SmallControllerArriveSpeed;
        job.SlowingRadius = Bootstrap.MoveSettings.SmallControllerStopRadius;

        job.SmallControllerPos = SmallController.Positions;
        job.Velocities = SmallController.Velocities;
        handle = job.Schedule(SmallController.Length, 64, inputDeps);
        //    }
        //}

        return handle;
    }
}
