using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeComponent : PursuitComponent
{
    protected override void ApplySteer(Vector2 sourcePos)
    {
        Steer = SteeringBehavior.Evade(sourcePos, Velocity, MovebleEntity.transform.position, MovebleEntity.Velocity);
    }
}
