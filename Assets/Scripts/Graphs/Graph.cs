using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SimpleJson;
using SimpleJSON;

namespace Graphs
{
    public class Graph<N, E> where N : GraphNode where E : GraphEdge
    {
        private List<N> Nodes = new List<N>();
        private Dictionary<int, List<E>> Edges = new Dictionary<int, List<E>>();
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
            StringBuilder stringBuilder = new StringBuilder();
            //string json = JsonUtility.ToJson(Nodes[0]);
            foreach (var node in Nodes)
            {
                stringBuilder.Append(JsonUtility.ToJson(node));
            }

            foreach (var edgeList in Edges)
            {
                foreach (var edge in edgeList.Value)
                {
                    stringBuilder.Append(JsonUtility.ToJson(edge));
                }
            }
            JSONClass lol = new JSONClass();
            lol ["ll"].AsInt = 15;
            lol["wow"] = "wowwo";
            string savePath = Application.streamingAssetsPath + "/lol.json";
            lol.SaveToFile(savePath);
            JSONNode someNode = JSONNode.LoadFromFile(savePath);
           
            Debug.LogError(someNode);
        }
    }
}