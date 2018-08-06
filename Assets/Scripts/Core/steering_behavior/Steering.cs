using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public struct Steering
{
    public float MaxSpeed;

    public float2 Arrive(float2 sourcePos, float2 targetPos, float slowingRadius = 1)
    {
        float2 desiredVelocity = targetPos - sourcePos;
        float distance = math.length(desiredVelocity);

        if (distance < slowingRadius)
            return new float2(0, 0);

        float timeToTarget = 0.25f;
        desiredVelocity /= timeToTarget;
        if (math.length(desiredVelocity) > MaxSpeed)
        {
            desiredVelocity = math.normalize(desiredVelocity);
            desiredVelocity *= MaxSpeed;
        }

        return desiredVelocity;
    }

    public float2 Seek(float2 sourcePos, float2 targetPos)
    {
        return targetPos - sourcePos;
    }
}
