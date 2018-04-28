using Graphs;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace GraphsTests
{
    public class GraphClassTest
    {
        [Test]
        public void TestAddNodes()
        {
            Graph<NavGraphNode, GraphEdge> graph = new Graph<NavGraphNode, GraphEdge>();
            int nodeQuntity = 10;
            for (int i = 0; i < nodeQuntity; i++)
            {
                graph.AddNode(new NavGraphNode(i,Vector2.down, ExtraInfoEnum.None));
            }

            Assert.AreEqual(nodeQuntity, graph.GetNodesQuantity());
        }
        
        [Test]
        public void TestAddEdge()
        {
            Graph<NavGraphNode, GraphEdge> graph = new Graph<NavGraphNode, GraphEdge>();
            int nodeQuntity = 10;
            for (int i = 0; i < nodeQuntity; i++)
            {
                graph.AddNode(new NavGraphNode(i,Vector2.down, ExtraInfoEnum.None));
            }

            Assert.AreEqual(nodeQuntity, graph.GetNodesQuantity());
        }
    }
}