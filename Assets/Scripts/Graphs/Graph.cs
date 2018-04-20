using System.Collections.Generic;

namespace Graphs
{
    public class Graph<N, E> where N : GraphNode where E : GraphEdge
    {
        private List<N> Nodes = new List<N>();
        private List<List<E>> Edges = new List<List<E>>();
        private int NextNodeIndex;

        public void AddNode(N node)
        {
            Nodes.Add(node);
            NextNodeIndex++;
        }

        public void AddEdge(E edge)
        {
            if (Edges[NextNodeIndex] == null)
                Edges[NextNodeIndex] = new List<E>();
            Edges[NextNodeIndex].Add(edge);
        }

        public void AddEdge(E edge, int nodeIndex)
        {
            if (Edges[nodeIndex] == null)
                Edges[nodeIndex] = new List<E>();
            Edges[nodeIndex].Add(edge);
        }

        public N GetNode(int index)
        {
            return Nodes[index];
        }

        /*   public E GetEdge(int index)
           {
               return Edges[index];
           }*/

        public int GetNextFreeNodeIndex()
        {
            return Nodes.Count;
        }
    }
}