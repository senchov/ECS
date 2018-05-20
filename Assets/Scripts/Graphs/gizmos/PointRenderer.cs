using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class PointRenderer : MonoBehaviour
    {
        [SerializeField] private float Radius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, Radius);
        }
    }
}