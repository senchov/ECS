using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class NavGraphCreator : MonoBehaviour
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private float MaxDistanceToNode;
        [SerializeField] private GameObject NavGraphPoint;

        private Graph<NavGraphNode, GraphEdge> Graph;

        private Vector3[] Directions = new[] {Vector3.right, Vector3.left, Vector3.down, Vector3.up};
        private List<GameObject> Points;

        [ContextMenu("CreateGraph")]
        public void CreateGraph()
        {
            Graph = new Graph<NavGraphNode, GraphEdge>();

            for (int i = 0; i < Directions.Length; i++)
            {
                if (!IsFindObstacle(Directions[i]))
                {
                    Vector3 pos = transform.position + MaxDistanceToNode * Directions[i];
                    GameObject point = Instantiate(NavGraphPoint, pos, Quaternion.identity);
                    Points.Add(point);
                    point.SetActive(true);
                }
            }
        }

        [ContextMenu("ClearPoints")]
        public void ClearPoints()
        {
            foreach (GameObject point in Points)
            {
                Destroy(point);
            }

            Points.Clear();
        }

        private bool IsFindObstacle(Vector3 dir)
        {
            return Physics.Raycast(transform.position, dir, MaxDistanceToNode);
        }
    }
}