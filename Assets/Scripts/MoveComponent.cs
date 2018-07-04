using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxVel = 0.2f;

    private SteeringBehavior SteeringBehavior;
    private Vector3 Velocity = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector3 Steer = Vector3.zero;

    private void Start()
    {
        SteeringBehavior = new SteeringBehavior(MaxSpeed, MaxVel);
        Target = transform.position;
    }

    private void Update()
    {
        SteeringBehavior.MaxSpeed = MaxSpeed;
        SteeringBehavior.MaxVelocity = MaxVel;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.LogError("mousePos" + mousePos);
            mousePos.z = 0;
            Target = mousePos;
        }

        Vector2 sourcePos = new Vector2(transform.position.x, transform.position.y);
        Steer = SteeringBehavior.Arrive(sourcePos, Target, Velocity);
        Debug.LogError("st->" + Steer.ToString());

        Velocity = Steer * Time.deltaTime;

        Truncate(ref Velocity, MaxVel);
        this.transform.position += Velocity;
    }

    private void Truncate(ref Vector3 velocity, float maxMagnitude)
    {
        float value = Mathf.Clamp01(maxMagnitude / velocity.magnitude);
        velocity = velocity * value;
    }
}