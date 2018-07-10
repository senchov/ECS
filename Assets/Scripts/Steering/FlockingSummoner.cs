using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingSummoner : MonoBehaviour {

    [SerializeField] private GameObject WanderEntity;
    [SerializeField] private float DistanceToNeiborhoods = 2.0f;
    [SerializeField] private float MinDistanceToNeiborhoods = 0.1f;
    [SerializeField] private Weights Weight;
    [SerializeField] private float LineLenght = 2.0f;
    [SerializeField] private List<Color> DebugColors;

    [Header("WanderBehavior")]
    [SerializeField] private float WanderRadius = 2.0f;
    [SerializeField] private float WanderDistance = 2.0f;
    [SerializeField] private float Jitter = 10.0f;
    [SerializeField] private float WanderSpeed = 10.0f;
    [SerializeField] private float MaxVel = 10.0f;
    [SerializeField] private float RotateSmooth = 5.0f;
   
    private FlockingBehavior Flocking = new FlockingBehavior();    
    List<Transform> FlockingEntities = new List<Transform>();
    List<WanderBehavior> WanderBehaviors = new List<WanderBehavior>();
    List<Vector2> Velocities = new List<Vector2>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CreateEntity();
        }

        AddWanderBehavior();

        for (int outer = 0; outer < FlockingEntities.Count; outer++)
        {
            List<Vector2> neiborVelocities = new List<Vector2>();
            List<Vector2> neiborPositions = new List<Vector2>();
            for (int inner = 0; inner < FlockingEntities.Count; inner++)
            {
                if (outer != inner)
                {
                    float distance = Vector3.Distance(FlockingEntities[outer].position, FlockingEntities[inner].position);
                    if (distance < DistanceToNeiborhoods)
                    {
                        neiborVelocities.Add(Velocities[inner]);
                        neiborPositions.Add(FlockingEntities[inner].position);
                    }
                }
            }
            Vector2 alignmentForce = Flocking.Alignment(FlockingEntities[outer].position, neiborVelocities);
            Vector2 cohesionForce = Flocking.Cohesion(FlockingEntities[outer].position, neiborPositions);
            Vector2 separationForce = Flocking.Separation(FlockingEntities[outer].position, neiborPositions);
            // Debug.DrawLine(Vector3.zero,alignmentForce,DebugColors [outer]);
            Velocities[outer] += alignmentForce * Weight.Alignment;
            Velocities[outer] += cohesionForce * Weight.Cohesion;
            Velocities[outer] += separationForce * Weight.Separation;
        }

        ConsumeVelocities();
    }

    private void ConsumeVelocities()
    {
        for (int i = 0; i < FlockingEntities.Count; i++)
        {
            Velocities[i] = Velocities[i] * Time.deltaTime;
            Velocities[i] = Vector3.ClampMagnitude(Velocities[i], MaxVel);
         //   Debug.LogError(Velocities[i].magnitude);
            Move(FlockingEntities[i], Velocities[i]);
            Rotate(FlockingEntities[i], Velocities[i]);
        }
    }

    private void AddWanderBehavior()
    {
        for (int i = 0; i < FlockingEntities.Count; i++)
        {
            Vector3 vel = new Vector3(Velocities[i].x, Velocities[i].y, 0);
            Vector2 wanderForce = WanderBehaviors[i].Wander(ref vel);

            ChangeColor(i);
            Velocities[i] = wanderForce * WanderSpeed;
        }
    }

    private void CreateEntity()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        GameObject entity = Instantiate(WanderEntity, mousePos, Quaternion.identity);
        FlockingEntities.Add(entity.transform);
        
        entity.SetActive(true);
        WanderBehaviors.Add(new WanderBehavior(WanderRadius, WanderDistance, Jitter));
        Velocities.Add(Vector2.zero);
        Color debugColor = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
        DebugColors.Add(debugColor);
    }

    private void ChangeColor(int i)
    {
        Renderer mat = FlockingEntities[i].GetComponent<Renderer>();
        mat.material.color = DebugColors[i];
    }

    private void Move(Transform target, Vector3 velocity)
    {
        target.position += velocity;
       /* Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width,Screen.height,0));       
        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));*/       
    }

    private void Rotate(Transform target, Vector3 velocity)
    {
        float angle = Mathf.Atan2(-velocity.x, velocity.y) * Mathf.Rad2Deg;
        Quaternion desireRotation = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, angle);
        target.rotation = desireRotation; //Quaternion.Lerp(target.rotation, desireRotation, RotateSmooth * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        foreach (Transform boid in FlockingEntities)
        {
            Gizmos.DrawWireSphere(boid.position, DistanceToNeiborhoods);
        }
    }

    [Serializable]
    private class Weights
    {
        public float Alignment = 1.0f;
        public float Cohesion = 1.0f;
        public float Separation = 1.0f;
    }
}
