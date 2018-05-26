using System;
using UnityEngine;

namespace Graphs
{
    public class NavGraphNode : GraphNode, IComparable
    {
        //estimated heuristic host from this node to goal node
        public float Hcost;

        //cost from start node to this node
        public float Gcost;

        public NavGraphNode Parent;

        public NavGraphNode(int index) : base(index)
        {
            Index = index;
        }

        public NavGraphNode(int index, Vector2 pos, ExtraInfoEnum info = ExtraInfoEnum.None) : base(index)
        {
            Position = pos;
            ExtraInfo = info;
        }

        public Vector2 Position;
        public ExtraInfoEnum ExtraInfo;

        public int CompareTo(object obj)
        {
            NavGraphNode node = (NavGraphNode) obj;
            if (Hcost < node.Hcost)
                return -1;
            if (Hcost > node.Hcost)
                return 1;
            return 0;
        }
    }
}