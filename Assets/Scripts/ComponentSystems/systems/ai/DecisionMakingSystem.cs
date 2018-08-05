using Unity.Entities;
using Unity.Collections;
using UnityEngine;

public class DecisionMakingSystem : ComponentSystem
{
    private struct DistanceToPlayerGroup
    {
        public readonly int Length;
        public ComponentDataArray<DistanceToPlayer> Distances;        
    }

    [Inject] DistanceToPlayerGroup Agents;

    protected override void OnStartRunning()
    {
        
    }

    protected override void OnUpdate()
    {
        for (int i = 0; i < Agents.Length; i++)
        {            
        }
    }
}
