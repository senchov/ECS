namespace Graphs
{
    public class GraphEdge
    {
        public int From;
        public int To;
        public float Cost;

        public GraphEdge(int from, int to, float cost)
        {
            From = from;
            To = to;
            Cost = cost;
        }

        public GraphEdge(int from, int to)
        {
            From = from;
            To = to;
            Cost = 1.0f;
        }
    }
}