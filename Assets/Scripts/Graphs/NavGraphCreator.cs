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
        [SerializeField] private int MaxNodeQuantity = 100;

        private Graph<NavGraphNode, GraphEdge> Graph;

        private Vector2[] Directions = new[] {Vector2.right, Vector2.left, Vector2.down, Vector2.up};
        private List<GameObject> Points = new List<GameObject>();

        [ContextMenu("CreateGraph")]
        public void CreateGraph()
        {
            Graph = new Graph<NavGraphNode, GraphEdge>();

            /*for (int i = 0; i < Directions.Length; i++)
            {
                if (!IsFindObstacle(Directions[i]))
                {
                    Vector3 pos = transform.position + MaxDistanceToNode * Directions[i];
                    GameObject point = Instantiate(NavGraphPoint, pos, Quaternion.identity);
                    Points.Add(point);
                    point.SetActive(true);
                }
            }*/

            Queue<NodeInfo> nodeQueue = new Queue<NodeInfo>();
            NodeInfo nodeInfo = new NodeInfo(0, StartPoint.position);
            nodeQueue.Enqueue(nodeInfo);
            int nodesQuantity = 0;

            while (nodeQueue.Count > 0 && nodesQuantity < MaxNodeQuantity)
            {
                NodeInfo info = nodeQueue.Dequeue();
                AddNodeToGraph(info.Index, info.Pos);
                CreatePoint(info.Pos);
                for (int i = 0; i < Directions.Length; i++)
                {
                    if (!IsFindObstacle(info.Pos, Directions[i]))
                    {
                        Vector2 pos = info.Pos + MaxDistanceToNode * Directions[i];
                        NodeInfo newNodeInfo = new NodeInfo(++nodesQuantity,pos);
                        nodeQueue.Enqueue(newNodeInfo);
                    }
                }

                
                if (nodesQuantity >= MaxDistanceToNode)
                    Debug.Log("to many nodes");
            }
        }

        private void AddNodeToGraph(int index, Vector2 pos)
        {
            NavGraphNode node = new NavGraphNode(index, pos, ExtraInfoEnum.None);
            Graph.AddNode(node);
        }

        private void CreatePoint(Vector3 pos)
        {
            GameObject point = Instantiate(NavGraphPoint, pos, Quaternion.identity);
            Points.Add(point);
            point.SetActive(true);
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

        private bool IsFindObstacle(Vector3 position, Vector3 dir)
        {
            return Physics.Raycast(position, dir, MaxDistanceToNode);
        }

        private struct NodeInfo
        {
            public NodeInfo(int index, Vector2 pos)
            {
                Index = index;
                Pos = pos;
            }

            public int Index;
            public Vector2 Pos;
        }
    }
}