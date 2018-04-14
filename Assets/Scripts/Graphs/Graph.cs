using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class Graph<N, E> where N : GraphNode where E : GraphEdge
    {
        private List<N> Nodes = new List<N>();
        private List<List<E>> Edges = new List<List<E>>();

        public void AddNode(N node)
        {
            Nodes.Add(node);
        }

        public void AddEdge(E edge)
        {
            Edges.Last().Add(edge);
        }

        public void AddEdge(E edge,int nodeIndex)
        {
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