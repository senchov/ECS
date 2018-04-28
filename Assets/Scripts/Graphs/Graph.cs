using System.Collections.Generic;

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

        public void AddEdge(E edge)
        {
            AddEdge(edge, NextNodeIndex);
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

        public int GetNextFreeNodeIndex()
        {
            return Nodes.Count;
        }

        public int GetNodesQuantity()
        {
            return Nodes.Count;
        }
    }
}