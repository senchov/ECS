using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitComponent : MonoBehaviour {

    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxVel = 0.2f;
    [SerializeField] private float MinVelocity = 0.05f;
    [SerializeField] protected MoveComponent MovebleEntity;

    protected SteeringBehavior SteeringBehavior;
    protected Vector3 Velocity = Vector3.zero;  
    protected Vector3 Steer = Vector3.zero;   

    private void Start()
    {
        SteeringBehavior = new SteeringBehavior(MaxSpeed, MaxVel);        
    }

    private void Update()
    {
        SteeringBehavior.MaxSpeed = MaxSpeed;
        SteeringBehavior.MaxVelocity = MaxVel;

        Vector2 sourcePos = new Vector2(transform.position.x, transform.position.y);
        ApplySteer(sourcePos);

        //Debug.LogError("st->" + Steer.ToString());

        Velocity = Steer * Time.deltaTime;

        Velocity = Vector3.ClampMagnitude(Velocity, MaxVel);
        if (Velocity.sqrMagnitude > MinVelocity)
        {
            transform.position += Velocity;
            Rotate();
        }
    }

    protected virtual void ApplySteer(Vector2 sourcePos)
    {
        Steer = SteeringBehavior.Pursuit(sourcePos, MovebleEntity.transform.position, MovebleEntity.Velocity);
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(-Velocity.x, Velocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }
}
