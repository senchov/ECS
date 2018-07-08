using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowerComponent : MonoBehaviour {

    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxVel = 0.2f;
    [SerializeField] private PathFollowManager FollowManager;
    [SerializeField] private float RotateSmooth;

    private SteeringBehavior SteeringBehavior;
    private PathFollowingBehavior FollowingBehavior;
    private Vector3 Velocity = Vector3.zero;    
    private Vector3 Steer = Vector3.zero;    
    
    private void Start()
    {
        SteeringBehavior = new SteeringBehavior(MaxSpeed, MaxVel);
        FollowingBehavior = new PathFollowingBehavior(SteeringBehavior, FollowManager.GetPointRadius(), true);
    }    

    private void Update()
    {
        SteeringBehavior.MaxSpeed = MaxSpeed;
        SteeringBehavior.MaxVelocity = MaxVel;
        FollowingBehavior.Radius = FollowManager.GetPointRadius();

        Vector2 sourcePos = new Vector2(transform.position.x, transform.position.y);        
        Steer = FollowingBehavior.Follow(sourcePos, FollowManager.Path);     

        Velocity = Steer * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, MaxVel);      

        this.transform.position += Velocity;
        Rotate();
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(-Velocity.x, Velocity.y) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, RotateSmooth*Time.deltaTime);  
    }
}
