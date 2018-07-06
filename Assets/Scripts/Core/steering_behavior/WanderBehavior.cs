using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior
{
    public float Radius;
    public float Distance;
    public float Jitter;

    Vector2 WanderTarget;
    float WanderAngle;

    public WanderBehavior(float radius, float distance, float jitter)
    {
        Radius = radius;
        Distance = distance;
        Jitter = jitter;
    }

    public Vector2 Wander(ref Vector3 velocity)
    {
        Vector2 circleCentr = velocity;
        circleCentr = circleCentr.normalized;
        circleCentr *= Distance;

        Vector2 displacement = new Vector2(0,-1);
        displacement *= Radius;

        SetAngle(ref displacement,WanderAngle);

        WanderAngle += Random.Range(-1.0f, 1.0f) * Jitter;

        return circleCentr + displacement;
    }

    private void SetAngle(ref Vector2 displacement, float wanderAngle)
    {
        float lenght = displacement.magnitude;
        displacement.x = Mathf.Cos(wanderAngle) * lenght;
        displacement.y = Mathf.Sin(wanderAngle) * lenght;
    }
   
}
