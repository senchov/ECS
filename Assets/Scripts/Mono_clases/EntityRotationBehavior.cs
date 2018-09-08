using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class EntityRotationBehavior : MonoBehaviour {

    [SerializeField] private GameObjectEntity GOEntity;
    [SerializeField] private EntityManagerProviderSO ManagerProvider;
    

    public void SetEntityRotation (quaternion targetRotation)
    {
        Rotation rotation = new Rotation();
        rotation.Value = targetRotation;       

        ManagerProvider.GetEntityManager.SetComponentData(GOEntity.Entity, rotation);
    }
}
