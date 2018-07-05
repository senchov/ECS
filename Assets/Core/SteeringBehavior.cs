using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{
    public float MaxSpeed;
    public float MaxVelocity;

    public Vector2 WanderTarget;

    public SteeringBehavior(float maxSpeed, float maxVelocity)
    {
        MaxSpeed = maxSpeed;
        MaxVelocity = maxVelocity;
    }

    public Vector3 Seek(Vector2 sourcePos, Vector2 target)
    {
        Vector2 desiredVelocity = (target - sourcePos).normalized * MaxSpeed;
        return desiredVelocity;
    }

    public Vector3 Seek(Vector2 sourcePos, Vector2 target, Vector2 sourceVel)
    {
        Vector2 desiredVelocity = (target - sourcePos).normalized * MaxSpeed;
        return desiredVelocity - sourceVel;
    }

    public Vector3 Arrive(Vector2 sourcePos, Vector2 target, Vector2 sourceVel, float slowingRadiusSqr = 1)
    {
        Vector2 desiredVelocity = target - sourcePos;
        float distance = desiredVelocity.sqrMagnitude;

        if (distance < slowingRadiusSqr)
            desiredVelocity = desiredVelocity.normalized  * (distance / slowingRadiusSqr) * MaxSpeed;
        else
        {
            desiredVelocity = desiredVelocity.normalized * MaxSpeed ;
        }
        
        return  desiredVelocity - sourceVel;
    }

    public Vector3 Flee(Vector2 sourcePos, Vector2 target, Vector2 sourceVel)
    {
        Vector2 desiredVelocity = (sourcePos - target).normalized * MaxSpeed;
        return desiredVelocity - sourceVel;
    }

    public Vector3 Wander(float now, float decisionTime, Vector2 sourcePos, Vector2[] targets,Vector2 sourceVel)
    {       
        if (now > decisionTime)
        {           
            WanderTarget = targets[Random.Range(0,targets.Length)];
        }
        
        return Arrive(sourcePos, WanderTarget, sourceVel);
    }   
}

public struct SteerParametrs
{
    public Vector2 Position;
    public Vector2 Target;
    public Vector2 Velocity;
}