using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace MovementSystem
{
    public class MoveDataComponent : MonoBehaviour
    {
        public Vector3 TargetPoint;
        public float Speed;
    }
}