using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{
    public float MaxSpeed;
    public float MaxVelocity;

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

    public Vector3 Arrive(Vector2 sourcePos, Vector2 target, Vector2 sourceVel, float slowingRaiusSqr = 1)
    {
        Vector2 desiredVelocity = target - sourcePos;
        float distance = desiredVelocity.sqrMagnitude;

        if (distance < slowingRaiusSqr)
            desiredVelocity = desiredVelocity.normalized  * (distance / slowingRaiusSqr) * MaxSpeed;
        else
        {
            desiredVelocity = desiredVelocity.normalized * MaxSpeed ;
        }
        
        return  desiredVelocity - sourceVel;
    }
}