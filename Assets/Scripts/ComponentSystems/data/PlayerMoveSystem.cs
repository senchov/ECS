using Unity.Entities;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem
{
    private struct Group
    {
        public InputData Input;
        public Transform PlayerTransform;
        public SpeedData SpeedDataComponent;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        foreach (Group entity in GetEntities<Group>())
        {
            Vector3 velocity = new Vector3(entity.Input.Horizontal, entity.Input.Vertical, 0);
            entity.PlayerTransform.position += velocity * deltaTime * entity.SpeedDataComponent.Speed;
        }
    }
}
