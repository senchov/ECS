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
        public float2 MousePos;

        public bool IsMousePressed;

        public float MaxSpeed;
        public float MaxArriveSpeed;
        public float SlowingRadius;
        public float MaxVelocity;

        public void Execute(int index)
        {
            Steering steering = new Steering();
            steering.MaxSpeed = MaxArriveSpeed;
            float2 sourcePos = new float2(SmallControllerPos[index].Value.x, SmallControllerPos[index].Value.y);
            float2 sourceVel = Velocities[index].Velocity;
           
            float2 targetPos = GetTargetPos(sourcePos);           
            float2 desireVelosity = steering.Arrive(sourcePos, targetPos, SlowingRadius);

            VelocityData data = new VelocityData();
            data.MaxSpeed = MaxSpeed;
            data.Velocity = desireVelosity;
            Velocities[index] = data;
        }

        private float2 GetTargetPos(float2 sourcePos)
        {
            float2 targetPos = new float2();
            if (IsMousePressed)
                targetPos = MousePos;
            else
                return BigControllerPos;

            float distanceToBigController = math.distance(MousePos, BigControllerPos);
            if (distanceToBigController > MaxVelocity)
            {
                float2 dir = math.normalize(MousePos - BigControllerPos) * MaxVelocity;
                targetPos = BigControllerPos + dir;
            }
            return targetPos;
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

    private bool IsMouseWasInCircleAndPressed;
    private bool IsMouseInCircle;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle handle = base.OnUpdate(inputDeps);
        InputData input = InputDataInjected.Inputs[0];

        float2 bigControllerPos = SetBigControllerPosition.Pos;
        DefineMousePosAndInput(input, bigControllerPos);

        SmallComponentVelocityJob job = new SmallComponentVelocityJob();      

        job.BigControllerPos = bigControllerPos;
        job.MousePos = input.MousePos;
        job.IsMousePressed = IsMouseWasInCircleAndPressed;

        job.MaxVelocity = SmallController.SmallControllers[0].MoveRadius;
        job.MaxSpeed = GetMaxSpeed();
        job.MaxArriveSpeed = SmallController.SmallControllers[0].SmallControllerArriveSpeed;
        job.SlowingRadius = SmallController.SmallControllers[0].SmallControllerStopRadius;

        job.SmallControllerPos = SmallController.Positions;
        job.Velocities = SmallController.Velocities;
        handle = job.Schedule(SmallController.Length, 64, inputDeps);

        return handle;
    }
  
    private void DefineMousePosAndInput(InputData input, float2 bigControllerPos)
    {
        if (input.IsMouse0Pressed && IsMouseInRange(input, bigControllerPos))
            IsMouseWasInCircleAndPressed = true;

        if (IsMouseWasInCircleAndPressed && !input.IsMouse0Pressed)
            IsMouseWasInCircleAndPressed = false;
    }

    private bool IsMouseInRange(InputData input, float2 bigControllerPos)
    {
        return math.distance(input.MousePos, bigControllerPos) < SmallController.SmallControllers[0].MoveRadius;
    }

    private float GetMaxSpeed()
    {
        float speed = 0;
        if (IsMouseWasInCircleAndPressed)
             speed = SmallController.SmallControllers[0].MoveToMouseSpeed;
        else
            speed = SmallController.SmallControllers[0].ComeBackSpeed;
        return speed;
    }
}
