using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSecondComponent : MonoBehaviour
{
    [SerializeField] private float WanderRadius = 2.0f;
    [SerializeField] private float WanderDistance = 2.0f;
    [SerializeField] private float Jitter = 10.0f;
    [SerializeField] private float MaxVel = 10.0f;   
    [SerializeField] private float WanderSpeed = 10.0f;
    [SerializeField] private int WanderFrequncy = 10;

    private WanderBehavior WanderBehavior;
    private Vector3 Steer = Vector3.zero;
    private Vector3 Velocity = Vector3.zero;
    private float RotateSmooth;
    private float MaxSmoothValue = 1.0f;

    private void Start()
    {
       WanderBehavior = new WanderBehavior(WanderRadius,WanderDistance, Jitter);
    }

    private void Update()
    {
        if (Time.frameCount % WanderFrequncy == 0)
            Steer = WanderBehavior.Wander(ref Velocity) * WanderSpeed;

        Velocity = Steer * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, MaxVel);      

        Move();
        Rotate();
    }

    private void Move()
    {
        transform.position += Velocity;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(-Velocity.x, Velocity.y) * Mathf.Rad2Deg;       
        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation,target, RotateSmooth);
        RotateSmooth += Time.deltaTime;
        if (RotateSmooth > MaxSmoothValue)
            RotateSmooth = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Velocity*WanderDistance);
    }
}
