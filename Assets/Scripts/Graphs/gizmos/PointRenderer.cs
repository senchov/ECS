using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class PointRenderer : MonoBehaviour
    {
        [SerializeField] private float Radius;
        public Vector3[] Points;
        
        private void OnDrawGizmos()
        {
          /*  for (int i = 0; i < Points.Length; i++)
            {
                Gizmos.DrawSphere(Points[i],Radius);
            }*/
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position,Radius);
        }
    }
}