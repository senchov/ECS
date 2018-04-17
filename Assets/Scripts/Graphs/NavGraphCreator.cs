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
        private Dictionary<int, List<int>> EdgeInfos = new Dictionary<int, List<int>>();
        private int PointIndex = 0;

        [ContextMenu("CreateGraph")]
        public void CreateGraph()
        {
            Queue<NodeInfo> nodeQueue = new Queue<NodeInfo>();
            NodeInfo nodeInfo = new NodeInfo(0, StartPoint.position);
            CreatePoint(StartPoint.position);
            nodeQueue.Enqueue(nodeInfo);
            int nodesQuantity = 0;
            int nodesIteration = 0;

            while (nodeQueue.Count > 0 && nodesQuantity < MaxNodeQuantity)
            {
                NodeInfo info = nodeQueue.Dequeue();
                EdgeInfos.Add(nodesIteration, new List<int>());
                for (int i = 0; i < Directions.Length; i++)
                {
                    if (!IsFindObstacle(info.Pos, Directions[i]))
                    {
                        Vector2 pos = info.Pos + MaxDistanceToNode * Directions[i];
                        NodeInfo newNodeInfo = new NodeInfo(++nodesQuantity, pos);
                        EdgeInfos[nodesIteration].Add(nodesQuantity);
                        CreatePoint(pos);
                        nodeQueue.Enqueue(newNodeInfo);
                    }
                }

               // CreateEdgeRenderers(nodesIteration, EdgeInfos[nodesIteration]);
                nodesIteration++;
                if (nodesQuantity >= MaxNodeQuantity)
                    Debug.LogError("to many nodes");
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
            point.gameObject.name += PointIndex++;
            point.SetActive(true);
        }

        private void CreateEdgeRenderers(int fromNode, List<int> toNodes)
        {
            for (int i = 0; i < toNodes.Count; i++)
            {
                EdgeRenderer edgeRenderer = Points[fromNode].GetComponent<EdgeRenderer>();
                if (!edgeRenderer)
                {
                    edgeRenderer = Points[fromNode].AddComponent<EdgeRenderer>();
                    edgeRenderer.StartNode = Points[fromNode].transform.position;
                }
                edgeRenderer.TargetNodes.Add(Points[toNodes[i]].transform.position);
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

        private bool IsFindObstacle(Vector3 position, Vector3 dir)
        {
            bool isFindObstacle = Physics.Raycast(position, dir, MaxDistanceToNode);
            return isFindObstacle;
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