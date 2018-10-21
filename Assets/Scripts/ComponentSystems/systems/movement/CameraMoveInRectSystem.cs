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
        public VelocityData PlayerVel;

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
            data.Velocity = new float2(0, 0);
            data.MaxSpeed = CameraVel.MaxSpeed;

            if (InRect)
                data.Velocity = PlayerVel.Velocity;
            
           // EntityBuffer.SetComponent(Entity, data);          
        }

        private bool InRect
        {
            get
            {
                if (PlayerPos.y > UpPoint || PlayerPos.y < DownPoint || PlayerPos.x > RightPoint || PlayerPos.x < LeftPoint)
                    return true;
                return false;
            }
        }
    }

    private struct PlayerGroup
    {
        public readonly int Length;
        public ComponentDataArray<PlayerData> Tag;
        [ReadOnly] public ComponentDataArray<Position> Pos;
        public ComponentDataArray<VelocityData> Vel;
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
        cameraInRectJob.EntityBuffer = Barrier.CreateCommandBuffer().ToConcurrent();
        cameraInRectJob.PlayerPos = Player.Pos[0].Value;
        cameraInRectJob.Entity = GameCamera.Entities[0];
        cameraInRectJob.CameraVel = GameCamera.Vel[0];
        cameraInRectJob.PlayerVel = Player.Vel[0];

        float leftPoint, rightPoint, downPoint, upPoint;
        GetBorderPoints(out leftPoint, out rightPoint, out downPoint, out upPoint);
        cameraInRectJob.UpPoint = upPoint;
        cameraInRectJob.DownPoint = downPoint;
        cameraInRectJob.RightPoint = rightPoint;
        cameraInRectJob.LeftPoint = leftPoint;

        // Debug.LogError("p->" + cameraInRectJob.PlayerPos.y + " u->" + upPoint);
        return cameraInRectJob.Schedule(inputDeps);
    }

    private void GetBorderPoints(out float leftPoint, out float rightPoint, out float downPoint, out float upPoint)
    {
        float highOffset = Bootstrap.CameraMoveSettings.RectangleSettings.HighOffset;
        float downOffset = Bootstrap.CameraMoveSettings.RectangleSettings.DownOffset;
        float rightOffset = Bootstrap.CameraMoveSettings.RectangleSettings.RightOffset;
        float leftOffset = Bootstrap.CameraMoveSettings.RectangleSettings.LeftOffset;

        float halfWidth = GetMainCamera.pixelWidth * 0.5f;
        float halfHeight = GetMainCamera.pixelHeight * 0.5f;

        Vector3 leftVector = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth - halfWidth * leftOffset, halfHeight));
        Vector3 rightVector = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth + halfWidth * rightOffset, halfHeight));
        Vector3 upVector = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth, halfHeight + halfHeight * highOffset));
        Vector3 downVector = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth, halfHeight - halfHeight * downOffset));

        leftPoint = leftVector.x;
        rightPoint = rightVector.x;
        downPoint = downVector.y;
        upPoint = upVector.y;
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
