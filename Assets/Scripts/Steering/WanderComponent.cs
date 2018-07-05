using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderComponent : MonoBehaviour
{
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxVel = 0.2f;
    [SerializeField] private Vector2[] WanderTargets;
    [SerializeField] private float DecisionTime = 3.0f;
    [SerializeField] private float MinVelocity = 0.05f;

    private SteeringBehavior SteeringBehavior;
    private Vector3 Velocity = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector3 Steer = Vector3.zero;

    private float WanderTime;


    private void Start()
    {
        SteeringBehavior = new SteeringBehavior(MaxSpeed, MaxVel);
        Target = transform.position;
        SteeringBehavior.WanderTarget = WanderTargets[0];
    }

    private void Update()
    {
        SteeringBehavior.MaxSpeed = MaxSpeed;
        SteeringBehavior.MaxVelocity = MaxVel;       

        Vector2 sourcePos = new Vector2(transform.position.x, transform.position.y);
        // Steer = SteeringBehavior.Flee(sourcePos, Target, Velocity);
        Steer = SteeringBehavior.Wander(WanderTime, DecisionTime, sourcePos, WanderTargets, Velocity);
        WanderTime = (WanderTime < DecisionTime) ? WanderTime += Time.deltaTime : 0;

        Debug.LogError("st->" + Steer.ToString());

        Velocity = Steer * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity,MaxVel);
        
        if (Velocity.sqrMagnitude < MinVelocity)
            Velocity = Vector3.zero;

        this.transform.position += Velocity;
    }
}


