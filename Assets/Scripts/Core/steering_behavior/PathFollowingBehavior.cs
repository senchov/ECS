using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingBehavior
{
    public float Radius;

    private SteeringBehavior Steering;
    private Vector2 Target;
    private int CurrentNode;
    private bool IsLoop;

    public PathFollowingBehavior(SteeringBehavior steering, float radius, bool isLoop = false)
    {
        Steering = steering;
        Radius = radius;
        IsLoop = isLoop;
    }

    public Vector3 Follow(Vector2 sourcePos,Vector2[] path)
    {
        if (path == null)
            return Vector2.zero;

        Target = path[CurrentNode];
        float distance = Vector2.Distance(sourcePos,Target);
        if (distance < Radius)
        {
            if (CurrentNode < path.Length - 1)
                CurrentNode++;
            else
                CurrentNode = 0;
        }            
        
        return Steering.Seek(sourcePos, Target);
    }    
}
