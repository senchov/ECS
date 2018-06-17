using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SimpleJSON;

namespace Graphs
{
    public class Graph<N, E> where N : GraphNode where E : GraphEdge
    {
        public List<N> Nodes = new List<N>();
        public Dictionary<int, List<E>> Edges = new Dictionary<int, List<E>>();
        private int NextNodeIndex;

        public void AddNode(N node)
        {
            Nodes.Add(node);
            Edges.Add(NextNodeIndex++, new List<E>());
        }

        public void AddEdge(E edge, int nodeIndex)
        {
            if (!Edges.ContainsKey(nodeIndex))
                Edges.Add(nodeIndex, new List<E>());

            Edges[nodeIndex].Add(edge);
        }

        public N GetNode(int index)
        {
            return Nodes[index];
        }

        public List<E> GetNodeEdges(int nodeIndex)
        {
            if (Edges.ContainsKey(nodeIndex))
                return Edges[nodeIndex];

            return null;
        }

        public List<int> GetConnectedNodesIds(int nodeindex)
        {
            List<int> connectedNodesIds = new List<int>();
            foreach (E edge in GetNodeEdges(nodeindex))
            {
                connectedNodesIds.Add(edge.To);
            }

            return connectedNodesIds;
        }

        public List<N> GetConnectedNodes(int nodeindex)
        {
            List<N> connectedNodes = new List<N>();
            foreach (E edge in GetNodeEdges(nodeindex))
            {
                connectedNodes.Add(GetNode(edge.To));
            }

            return connectedNodes;
        }

        public int GetNextFreeNodeIndex()
        {
            return Nodes.Count;
        }

        public int GetNodesQuantity()
        {
            return Nodes.Count;
        }

        public void SaveToFile(string path)
        {
            JSONClass graphJson = new JSONClass();

            graphJson["nodes"] = new JSONArray();
            foreach (N node in Nodes)
            {
                graphJson["nodes"].Add(JsonUtility.ToJson(node));
            }

            graphJson["edges"] = new JSONArray();
            foreach (var edgeList in Edges)
            {
                foreach (GraphEdge edge in edgeList.Value)
                {
                    graphJson["edges"].Add(JsonUtility.ToJson(edge));
                }
            }

            graphJson.SaveToFile(path);
        }

        public static Graph<N, E> LoadGraphFromFile(string path)
        {
            Graph<N, E> loadedGraph = new Graph<N, E>();
            JSONClass loadedJson = JSONNode.LoadFromFile(path).AsObject;
            foreach (JSONNode jsonNode in loadedJson["nodes"].Children)
            {
                N node = JsonUtility.FromJson<N>(jsonNode);
                loadedGraph.AddNode(node);
            }

            foreach (JSONNode jsonNode in loadedJson["edges"].Children)
            {
                E edge = JsonUtility.FromJson<E>(jsonNode);
                loadedGraph.AddEdge(edge, edge.From);
            }

            return loadedGraph;
        }
    }
}