using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class NavGraphCreator : MonoBehaviour
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private float MaxDistanceToNode;
        [SerializeField] private float MaxEdgeLenght;
        [SerializeField] private GameObject NavGraphPoint;
        [SerializeField] private int MaxNodeQuantity = 100;
        [SerializeField] private string NodeTag;

        private Graph<NavGraphNode, GraphEdge> Graph;

        private Vector2[] PosibleDirectionsForNode = new[] {Vector2.right, Vector2.left, Vector2.down, Vector2.up};

        private Vector2[] PosibleDirectionsForEdges = new[]
        {
            Vector2.right, Vector2.left, Vector2.down, Vector2.up,
            Vector2.one, new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

        private List<GameObject> Points = new List<GameObject>();
        private int PointIndex = 0;

        [ContextMenu("CreateGraph")]
        public void CreateGraph()
        {
            CreateVisualGraph();
            Graph = new Graph<NavGraphNode, GraphEdge>();
            for (int i = 0; i < Points.Count; i++)
            {
                NodeInfoComponent nodeInfo = Points[i].GetComponent<NodeInfoComponent>();
                Graph.AddNode(new NavGraphNode(nodeInfo.Index, Points[i].transform.position, nodeInfo.ExtraInfo));
                for (int j = 0; j < nodeInfo.ConnectedNodes.Count; j++)
                {
                    float distanceBetweenNodes = Vector2.Distance(Points[nodeInfo.Index].transform.position,
                        Points[nodeInfo.ConnectedNodes[j]].transform.position);
                    Graph.AddEdge(new GraphEdge(nodeInfo.Index, nodeInfo.ConnectedNodes[j], distanceBetweenNodes),
                        nodeInfo.Index);
                }
            }
            Graph.SaveToFile("");
        }

        [ContextMenu("CreateVisualGraph")]
        public void CreateVisualGraph()
        {
            CreateGraphNodes();
            CreateGraphEdges();
        }

        [ContextMenu("CreateGraphNodes")]
        public void CreateGraphNodes()
        {
            Queue<NodeInfo> nodeQueue = new Queue<NodeInfo>();
            NodeInfo nodeInfo = new NodeInfo(0, StartPoint.position);
            CreatePoint(StartPoint.position);
            nodeQueue.Enqueue(nodeInfo);
            int nodeInfoQuantity = 0;
            int nodesQuantity = 0;

            while (nodeQueue.Count > 0 && nodeInfoQuantity < MaxNodeQuantity)
            {
                NodeInfo info = nodeQueue.Dequeue();
                for (int i = 0; i < PosibleDirectionsForNode.Length; i++)
                {
                    if (!IsFindObstacle(info.Pos, PosibleDirectionsForNode[i]))
                    {
                        Vector2 pos = info.Pos + MaxDistanceToNode * PosibleDirectionsForNode[i];
                        NodeInfo newNodeInfo = new NodeInfo(++nodeInfoQuantity, pos);
                        CreatePoint(pos);
                        nodeQueue.Enqueue(newNodeInfo);
                    }
                }


                nodesQuantity++;

                if (nodeInfoQuantity >= MaxNodeQuantity)
                    Debug.LogError("to many nodes");
            }
        }

        [ContextMenu("CreateGraphEdges")]
        public void CreateGraphEdges()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                CreateEdgesToNode(i);
            }
        }

        private void CreatePoint(Vector3 pos)
        {
            GameObject point = Instantiate(NavGraphPoint, pos, Quaternion.identity);
            Points.Add(point);
            NodeInfoComponent nodeInfoComponent = point.AddComponent<NodeInfoComponent>();
            nodeInfoComponent.Index = PointIndex;
            point.gameObject.name += PointIndex++;
            point.SetActive(true);
        }

        private void CreateEdgesToNode(int node)
        {
            Vector3 nodePosition = Points[node].transform.position;
            for (int i = 0; i < PosibleDirectionsForEdges.Length; i++)
            {
                int toNodeIndex = GetNodeIndex(nodePosition, PosibleDirectionsForEdges[i]);
                if (toNodeIndex != -1)
                {
                    AddConectedNodes(node, toNodeIndex);
                    AddPointToRenderer(GetEdgeRenderer(node), toNodeIndex);
                }
            }
        }

        private void AddPointToRenderer(EdgeRenderer edgeRenderer, int toNode)
        {
            edgeRenderer.TargetNodes.Add(Points[toNode].transform.position);
        }

        private void AddConectedNodes(int node, int conectedNode)
        {
            NodeInfoComponent nodeInfoComponent = Points[node].GetComponent<NodeInfoComponent>();
            nodeInfoComponent.AddConnectedNode(conectedNode);
        }

        private EdgeRenderer GetEdgeRenderer(int fromNode)
        {
            EdgeRenderer edgeRenderer = Points[fromNode].GetComponent<EdgeRenderer>();
            if (!edgeRenderer)
            {
                edgeRenderer = Points[fromNode].AddComponent<EdgeRenderer>();
                edgeRenderer.StartNode = Points[fromNode].transform.position;
            }

            return edgeRenderer;
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

        private bool IsNode(RaycastHit hit)
        {
            return hit.transform.gameObject.tag.Equals(NodeTag);
        }

        private int GetNodeIndex(Vector3 position, Vector3 dir)
        {
            int nodeIndex = -1;
            RaycastHit hit;
            if (Physics.Raycast(position, dir, out hit, MaxEdgeLenght) && IsNode(hit))
            {
                NodeInfoComponent nodeInfoComponentComponent =
                    hit.transform.gameObject.GetComponent<NodeInfoComponent>();
                nodeIndex = nodeInfoComponentComponent.Index;
            }

            return nodeIndex;
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