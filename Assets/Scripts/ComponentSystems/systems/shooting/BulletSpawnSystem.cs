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

    protected override void OnUpdate()
    {
     //   NativeArray<Entity> entities = new NativeArray<Entity>();
        for (int i = 0; i < Data.Length; i++)
        {
            if (Data.Inputs[i].IsFire)
            {
                foreach (PlayerGroup player in GetEntities<PlayerGroup>())
                {
                    GameObject bullet = Object.Instantiate(Bootstrap.PrefabHub.Bullet);                   
                    bullet.transform.rotation = player.Transform.rotation;
                    bullet.transform.position = player.Transform.position; 
                    
                }  
            }
        }
    }
}
