using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class Lol : ComponentSystem
{
    struct Group
    {
        public Transform TargetTransform;
        public Rotator TargetRotator;
    }
    
    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;
        foreach (var item in GetEntities<Group>())
        {
           item.TargetTransform.rotation *= Quaternion.AngleAxis(item.TargetRotator.Speed*deltaTime,Vector3.up);
        }
    }
}