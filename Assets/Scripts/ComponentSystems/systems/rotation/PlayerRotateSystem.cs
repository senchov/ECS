using Unity.Entities;
using UnityEngine;

public class PlayerRotateSystem : ComponentSystem
{
    private struct PlayerVelocitySystemGroup
    {
        public Player Player;
        public Transform PlayerTransform;
        public RotateSmoothData RotateSmooth;
    }

    protected override void OnUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float deltaTime = Time.deltaTime;
        foreach (PlayerVelocitySystemGroup entity in GetEntities<PlayerVelocitySystemGroup>())
        {
            Vector3 direction = mousePos - entity.PlayerTransform.position;
            float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            Transform playerTransform = entity.PlayerTransform;
            Quaternion desireRotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, angle);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, desireRotation, deltaTime * entity.RotateSmooth.Smooth);
        }
    }
}
