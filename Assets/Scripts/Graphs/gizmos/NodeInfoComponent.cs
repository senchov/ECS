using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class NodeInfoComponent : MonoBehaviour
    {
        public ExtraInfoEnum ExtraInfo = ExtraInfoEnum.None;
        public int Index;
        public List<int> ConnectedNodes = new List<int>();

        public void AddConnectedNode(int connectedNode)
        {
            ConnectedNodes.Add(connectedNode);
        }
    }
}