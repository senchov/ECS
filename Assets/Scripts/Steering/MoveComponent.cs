using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxVel = 0.2f;  
    [SerializeField] private float MinVelocity = 0.05f;
    [SerializeField] private float RotateSmooth = 5.0f;

    public float LineLenght = 10;

    private SteeringBehavior SteeringBehavior;
    public Vector3 Velocity = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector3 Steer = Vector3.zero;

    private float WanderTime;

    private void Start()
    {
        SteeringBehavior = new SteeringBehavior(MaxSpeed, MaxVel);
        Target = transform.position;       
    }

    private void Update()
    {
        SteeringBehavior.MaxSpeed = MaxSpeed;
        SteeringBehavior.MaxVelocity = MaxVel;

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // Debug.LogError("mousePos" + mousePos);
            mousePos.z = 0;
            Target = mousePos;
        }

        Vector2 sourcePos = new Vector2(transform.position.x, transform.position.y);
        Steer = SteeringBehavior.Arrive(sourcePos, Target, Velocity);

        //  Debug.LogError("st->" + Steer.ToString());

        Velocity = Steer * Time.deltaTime;

        Truncate(ref Velocity, MaxVel);

        if (Velocity.sqrMagnitude > MinVelocity)
        {
            transform.position += Velocity;
            Rotate();
        }
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(-Velocity.x, Velocity.y) * Mathf.Rad2Deg;
        Quaternion desireRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation,desireRotation,Time.deltaTime * RotateSmooth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Target);
    }

    private void Truncate(ref Vector3 velocity, float maxMagnitude)
    {
        float value = Mathf.Clamp01(maxMagnitude / velocity.magnitude);
        velocity = velocity * value;
    }
}