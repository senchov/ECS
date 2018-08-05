using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SetDistanceToPlayerSystem : JobComponentSystem
{
    private struct SetDistanceToPlayerSystemJob : IJobParallelFor
    {
        public ComponentDataArray<DistanceToPlayer> Distances;
        public ComponentDataArray<Position> EntityPositions;
        public float3 PlayerPos;

        public void Execute(int index)
        {
            DistanceToPlayer distanceToPlayer = new DistanceToPlayer();
            distanceToPlayer.Distance = math.distance(PlayerPos, EntityPositions[index].Value);
            Distances[index] = distanceToPlayer;
        }
    }

    private struct DistanceToPlayerGroup
    {
        public readonly int Length;
        public ComponentDataArray<DistanceToPlayer> Distances;
        public ComponentDataArray<Position> Positions;
    }

    private struct PlayerGroup
    {
        public readonly int Length;
        public readonly ComponentDataArray<PlayerData> PlayerTag;        
        public readonly ComponentArray<Transform> Transforms;
    }

    [Inject] PlayerGroup Player;
    [Inject] DistanceToPlayerGroup ToPlayer;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        SetDistanceToPlayerSystemJob job = new SetDistanceToPlayerSystemJob();        
        job.PlayerPos = Player.Transforms[0].position;
        job.EntityPositions = ToPlayer.Positions;
        job.Distances = ToPlayer.Distances;
        return job.Schedule(ToPlayer.Length, 64, inputDeps);
    }
}
