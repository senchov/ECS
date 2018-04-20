using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class EdgeRenderer : MonoBehaviour
    {
        public Vector3 StartNode;
        public List<Vector3> TargetNodes = new List<Vector3>();
        [SerializeField] private Color EdgeColor = Color.white;

        private void OnDrawGizmos()
        {
            Gizmos.color = EdgeColor;
            foreach (Vector3 node in TargetNodes)
            {
                Gizmos.DrawLine(StartNode,node);
            }
        }
    }
}