using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public class BulletSpawnSystem : ComponentSystem
{
    private struct Group
    {
        public readonly int Length;
        public ComponentArray<InputData> Inputs;
    }

    [Inject] private Group Data;

    private struct PlayerGroup
    {
        public Player Player;
        public Transform Transform;
    }

    private struct BulletGroup
    {
        public Bullet BulletTag;
    }

    protected override void OnUpdate()
    {       
        for (int i = 0; i < Data.Length; i++)
        {
            if (Data.Inputs[i].IsFire)
            {
                foreach (PlayerGroup player in GetEntities<PlayerGroup>())
                {                   
                    Entity entity = EntityManager.Instantiate(Bootstrap.PrefabHub.Bullet);

                    float3 pos = new float3(player.Transform.position.x, player.Transform.position.y, 0);
                    EntityManager.SetComponentData(entity, new Position { Value = pos });

                    Bullet bullet = new Bullet();
                    bullet.RemoveAt = Time.time + Bootstrap.DestroySettings.BulletExistTime;
                    EntityManager.SetComponentData(entity,bullet);
                }
            }
        }
    }
}
