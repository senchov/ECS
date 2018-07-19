using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowComponent : MonoBehaviour
{
    [Header("Steering settings")]
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MaxVel = 0.2f;
    [SerializeField] private float RotateSmooth = 5.0f;
    [SerializeField] private float MoveSmooth = 10.0f;
    [SerializeField] private float DistanceToNeiborhoods = 10.0f;
    [SerializeField] private float MinVelocity = 0.01f;

    [Header("Follow settings")]
    [SerializeField] private GameObject FollowerPrefab;
    [SerializeField] private MoveComponent Leader;
    [SerializeField] private float Behind = 2.0f;
    [SerializeField] private Weights Weight;
    [SerializeField] private float MinSaparation = 0.01f;
    [SerializeField] private Transform[] SquadPositionsTransforms;

    List<Transform> Followers = new List<Transform>();
    List<Vector2> Velocities = new List<Vector2>();
    List<Vector2> SquadPositions = new List<Vector2>();

    private List<Color> DebugColors = new List<Color>();

    SteeringBehavior Steering;
    LeaderFollowBehavior FollowBehavior;
    private FlockingBehavior Flocking = new FlockingBehavior();

    private void Start()
    {
        Steering = new SteeringBehavior(MaxSpeed, MaxVel);
        FollowBehavior = new LeaderFollowBehavior(Steering, Behind);

        FillSquadPositions();
    }

    private void Update()
    {
        Steering.MaxSpeed = MaxSpeed;
        Steering.MaxVelocity = MaxVel;
        FollowBehavior.BehindDistance = Behind;

        if (Input.GetMouseButtonDown(1))
        {
            CreateEntity();
        }

        ApplyFollow();
        ApplySeparation();
        MoveFollowers();
    }

    private void FillSquadPositions()
    {
        foreach (Transform squadTransform in SquadPositionsTransforms)
        {
            SquadPositions.Add(squadTransform.position);
        }
    }

    private void CreateEntity()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        GameObject entity = Instantiate(FollowerPrefab, mousePos, Quaternion.identity);
        entity.SetActive(true);

        Followers.Add(entity.transform);
        Velocities.Add(Vector2.zero);
        ChangeColor(entity);
    }

    private void ApplySeparation()
    {
        for (int outer = 0; outer < Followers.Count; outer++)
        {
            List<Vector2> neiborPositions = new List<Vector2>();
            for (int inner = 0; inner < Followers.Count; inner++)
            {
                if (outer != inner)
                {
                    float distance = Vector3.Distance(Followers[outer].position, Followers[inner].position);
                    if (distance < DistanceToNeiborhoods)
                    {
                        neiborPositions.Add(Followers[inner].position);
                    }
                }
            }

            Vector2 separationForce = Flocking.Separation(Followers[outer].position, neiborPositions);
            // Debug.DrawLine(Vector3.zero,alignmentForce,DebugColors [outer]);

            if (separationForce.sqrMagnitude > MinSaparation)
                Velocities[outer] += separationForce * Weight.Separation;
        }
    }

    private void ApplyFollow()
    {
        for (int i = 0; i < Followers.Count; i++)
        {
            Vector2 sourcePos = Followers[i].position;
            Vector2 leaderPos = Leader.transform.position;
            Vector2 leaderVel = Leader.Velocity;
          //  Debug.LogError("leader->" + leaderPos + " vel->" + leaderVel);
            Vector2 followForce = FollowBehavior.Follow(sourcePos, Velocities[i], leaderPos, leaderVel);
            Velocities[i] += followForce * Weight.Follow;
          //  Debug.LogError("filledVel->" + Velocities[i]);
        }
    }

    private void MoveFollowers()
    {
        for (int i = 0; i < Followers.Count; i++)
        {
            Move(Followers[i], Velocities[i]);
            Rotate(Followers[i], Velocities[i]);
        }
    }

    private void Move(Transform target, Vector3 velocity)
    {
        if (velocity.sqrMagnitude > MinVelocity)
        {
            velocity = Vector3.ClampMagnitude(velocity, MaxVel);
            target.position += transform.position + velocity;
        }
    }

    private void Rotate(Transform target, Vector3 velocity)
    {
        float angle = Mathf.Atan2(-velocity.x, velocity.y) * Mathf.Rad2Deg;
        Quaternion desireRotation = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, angle);
        target.rotation = Quaternion.Lerp(target.rotation, desireRotation, RotateSmooth * Time.deltaTime);
    }

    private void ChangeColor(GameObject boid)
    {
        Renderer mat = boid.GetComponent<Renderer>();
        Color debugColor = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
        DebugColors.Add(debugColor);
        mat.material.color = debugColor;
    }

    [Serializable]
    private class Weights
    {
        public float Separation = 1.0f;
        public float Follow = 1.0f;
    }
}
