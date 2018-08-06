using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SetPursuitBehaviorDataSystem : ComponentSystem
{
   /* private struct SetPursuitBehaviorDataSystemJob : IJobParallelFor
    {
        public float2 PlayerVelocity;
        public float3 PlayerPos;
        public float MaxVelocity;

        public ComponentDataArray<PursuitData> Pursuits;

        public void Execute(int index)
        {
            PursuitData data = new PursuitData();
            data.Target = new float2(PlayerPos.x, PlayerPos.y);
            data.TargetVelocity = PlayerVelocity;
            data.MaxVelocity = Pursuits[index].MaxVelocity;
            Pursuits[index] = data;
        }
    }*/

    private struct PlayerGroup
    {
        public readonly int Length;
        public ComponentDataArray<PlayerData> PlayerTag;
        public ComponentDataArray<VelocityData> Velocities;
        public ComponentDataArray<Position> Positions;
    }

    private struct PursuitGroup
    {
        public readonly int Length;
        public ComponentDataArray<PursuitData> Pursuits;
    }

    [Inject] PlayerGroup Player;
    [Inject] PursuitGroup Pursuit;    

    protected override void OnUpdate()
    {
        float3 PlayerPos = Player.Positions[0].Value;
        float2 PlayerVelocity = Player.Velocities[0].Velocity;

        for (int i = 0; i < Pursuit.Length; i++)
        {            
            PursuitData data = new PursuitData();
            data.Target = new float2(PlayerPos.x, PlayerPos.y);
            data.TargetVelocity = PlayerVelocity;
            data.ArriveSpeed = Pursuit.Pursuits[i].ArriveSpeed;
            data.StopRadius = Pursuit.Pursuits[i].StopRadius;
            Pursuit.Pursuits[i] = data;
        }
    }
}
