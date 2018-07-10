using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingBehavior
{  
    public Vector3 Alignment(Vector2 sourcePos,List<Vector2> velocities)
    {
        Vector2 alignmentForce = Vector2.zero;
        if (velocities.Count < 1)
            return alignmentForce;

        foreach (Vector2 velocity in velocities)
        {            
            alignmentForce += velocity;                   
        }
        
        alignmentForce /= velocities.Count;

        return alignmentForce.normalized;
    }

    public Vector3 Cohesion(Vector2 sourcePos, List<Vector2> positions)
    {
        Vector2 cohesionForce = Vector2.zero;
        if (positions.Count < 1)
            return cohesionForce;

        foreach (Vector2 position in positions)
        {
            cohesionForce += position;
        }
       
        cohesionForce /= positions.Count;

        return (cohesionForce - sourcePos).normalized;
    }

    public Vector3 Separation(Vector2 sourcePos, List<Vector2> positions)
    {
        Vector2 separationForce = Vector2.zero;
        if (positions.Count < 1)
            return separationForce;

        foreach (Vector2 position in positions)
        {
            separationForce += position - sourcePos;
        }

        separationForce /= positions.Count;

        float negativeDirection = -1.0f;
        return (separationForce * negativeDirection).normalized;
    }
}

public class FlockingAgent
{
    public Vector2 Position;
    public Vector2 Velocity;

    public FlockingAgent(Vector2 position, Vector2 velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}


