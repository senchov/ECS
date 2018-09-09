using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class CameraMoveInRectSystem : JobComponentSystem
{
    private struct CameraInRectJob : IJob
    {
        public float3 PlayerPos;

        public VelocityData CameraVel;

        public float LeftPoint;
        public float RightPoint;
        public float DownPoint;
        public float UpPoint;
        public float MaxSpeed;

        public EntityCommandBuffer.Concurrent EntityBuffer;
        public Entity Entity;

        public void Execute()
        {
            VelocityData data = new VelocityData();
            data.Velocity = new float2(-1, 0);
            data.MaxSpeed = CameraVel.MaxSpeed;
                        
            EntityBuffer.SetComponent(Entity, data);
        }
    }

    private struct PlayerGroup
    {
        public readonly int Length;
        public ComponentDataArray<PlayerData> Tag;
        [ReadOnly] public ComponentDataArray<Position> Pos;
    }

    private struct CameraGroup
    {
        public readonly int Length;
        public EntityArray Entities;
        public ComponentDataArray<CameraTag> Tag;
        public ComponentDataArray<VelocityData> Vel;
    }

    [Inject] CameraMoveInRectSystemBarrier Barrier;
    [Inject] PlayerGroup Player;
    [Inject] CameraGroup GameCamera;

    // NativeArray<Position> CopyPlayerPositions;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        inputDeps.Complete();   

        CameraInRectJob cameraInRectJob = new CameraInRectJob();
        cameraInRectJob.EntityBuffer = Barrier.CreateCommandBuffer();
        cameraInRectJob.PlayerPos = Player.Pos[0].Value;
        cameraInRectJob.Entity = GameCamera.Entities[0];
        cameraInRectJob.CameraVel = GameCamera.Vel[0];
       
        return cameraInRectJob.Schedule(inputDeps);
    }    

    private void GetBorderPoints(out Vector3 leftPoint, out Vector3 rightPoint, out Vector3 downPoint, out Vector3 upPoint)
    {
        float highOffset = Bootstrap.CameraMoveSettings.RectangleSettings.HighOffset;
        float downOffset = Bootstrap.CameraMoveSettings.RectangleSettings.DownOffset;
        float rightOffset = Bootstrap.CameraMoveSettings.RectangleSettings.RightOffset;
        float leftOffset = Bootstrap.CameraMoveSettings.RectangleSettings.LeftOffset;

        float halfWidth = GetMainCamera.pixelWidth * 0.5f;
        float halfHeight = GetMainCamera.pixelHeight * 0.5f;

        leftPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth - halfWidth * leftOffset, halfHeight));
        rightPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth + halfWidth * rightOffset, halfHeight));
        downPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth, halfHeight + halfHeight * downOffset));
        upPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth, halfHeight - halfHeight * highOffset));
    }

    private Camera MainCamera;

    private Camera GetMainCamera
    {
        get
        {
            if (MainCamera == null)
                MainCamera = Camera.main;
            return MainCamera;
        }
    }
}

public class CameraMoveInRectSystemBarrier : BarrierSystem
{
}
