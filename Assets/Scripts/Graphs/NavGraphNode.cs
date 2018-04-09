using UnityEngine;

namespace Graphs
{
    public class NavGraphNode : GraphNode
    {
        public NavGraphNode(int index) : base(index)
        {
        }

        public NavGraphNode(int index,Vector2 pos, ExtraInfoEnum info) : base(index)
        {
            Position = pos;
            ExtraInfo = info;
        }

        public Vector2 Position;
        public ExtraInfoEnum ExtraInfo;
    }
}