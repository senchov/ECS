using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class GraphHolder : MonoBehaviour
    {
        [SerializeField] private string GraphFileName;
        [SerializeField] private GameObject NavGraphNodeOnScene;

        Graph<NavGraphNode, GraphEdge> Graph = new Graph<NavGraphNode, GraphEdge>();
        private List<GameObject> NavGraphNodeOnSceneList = new List<GameObject>();

        [ContextMenu("RenderGraph")]
        public void RenderGraph()
        {
            RenderGraph(GraphFileName);
        }

        public void RenderGraph(string path)
        {
            Graph = Graph<NavGraphNode, GraphEdge>.LoadGraphFromFile(GetPath());
            foreach (NavGraphNode graphNode in Graph.Nodes)
            {
                GameObject point = Instantiate(NavGraphNodeOnScene, graphNode.Position, Quaternion.identity);
                point.transform.SetParent(this.transform);
                point.SetActive(true);
                NavGraphNodeOnSceneList.Add(point);
            }

            print(NavGraphNodeOnSceneList.Count);
        }

        private string GetPath()
        {
            return Application.streamingAssetsPath + "/" + GraphFileName;
        }

        private Graph<NavGraphNode, GraphEdge> LoadGraph()
        {
            return Graph<NavGraphNode, GraphEdge>.LoadGraphFromFile(GraphFileName);
        }
    }
}