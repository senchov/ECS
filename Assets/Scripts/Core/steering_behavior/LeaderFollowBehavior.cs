using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowBehavior
{
    SteeringBehavior Steering;
    private float DirectionFromLeader = -1.0f;
    private float LeaderBehindDistance;

    public LeaderFollowBehavior(SteeringBehavior steering, float leaderBehindDistance)
    {
        Steering = steering;
        LeaderBehindDistance = leaderBehindDistance;
    }

    public float Direction
    {
        set { DirectionFromLeader = value; }
    }

    public float BehindDistance
    {
        set { LeaderBehindDistance = value; }
    }

    public Vector2 Follow(Vector2 sourcePos,Vector2 sourceVel, Vector2 leaderPos, Vector2 leaderVelocity)
    {
        Vector2 targetVelocity = leaderVelocity * DirectionFromLeader;
        targetVelocity = targetVelocity.normalized;
        targetVelocity *= LeaderBehindDistance;
        Vector2 behind = leaderPos + targetVelocity;
        
        return Steering.Arrive(sourcePos, behind, sourceVel);
    }
}
