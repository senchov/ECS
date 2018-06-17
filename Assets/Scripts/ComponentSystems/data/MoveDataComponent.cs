using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    public class MoveDataComponent : MonoBehaviour
    {
        public int LastVisitedNode;
        public Vector3 TargetPoint;
        public float Speed;
    }
}