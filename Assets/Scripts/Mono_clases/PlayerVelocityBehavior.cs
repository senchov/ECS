using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class PlayerVelocityBehavior : MonoBehaviour
{
    [SerializeField] private GameObjectEntity PlayerEntity;
    [SerializeField] private EntityManagerProviderSO ManagerProvider;    

    public void SetPlayerVelocity(float2 vel)
    {        
        VelocityData velData = ManagerProvider.GetEntityManager.GetComponentData<VelocityData>(PlayerEntity.Entity);
        velData.Velocity = vel;   
        ManagerProvider.GetEntityManager.SetComponentData(PlayerEntity.Entity, velData);
    }

    [ContextMenu("set")]
    public void SetVelocityDebug()
    {
        SetPlayerVelocity(new float2 (1.0f,0));
    }
}
