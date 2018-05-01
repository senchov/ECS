using Graphs;
using NUnit.Framework;
using SimpleJSON;
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
            int[] edgeNodes = { 5, 7, 8 };
            int node = 5;
            for (int i = 0; i < edgeNodes.Length; i++)
            {
                graph.AddEdge(new GraphEdge(5,edgeNodes[i]),node);
            }

            GraphEdge expectEdge = new GraphEdge(node,edgeNodes[2]);
            GraphEdge actualEdge = graph.GetNodeEdges(node)[2];
            Assert.AreEqual(expectEdge.To, actualEdge.To);
            Assert.AreEqual(expectEdge.From, actualEdge.From);
        }

        [Test]
        public void SaveFileTest()
        {
            Graph<NavGraphNode, GraphEdge> graph = new Graph<NavGraphNode, GraphEdge>();
            //string expected = "{"nodes":[{"Position":{"x":80.0,"y":58.8},"ExtraInfo":0},{"Position":{"x":95.0,"y":58.8},"ExtraInfo":0}],"edges":[{"From":0,"To":1,"Cost":15.0},{"From":0,"To":2,"Cost":15.0},{"From":1,"To":4,"Cost":21.2},{"From":2,"To":3,"Cost":21.2},{"From":3,"To":0,"Cost":15.0},{"From":3,"To":1,"Cost":21.2}]}";
            JSONClass lol = new JSONClass();
            lol ["ll"].AsInt = 15;
            lol["wow"] = "wowwo";
            lol.SaveToFile(Application.streamingAssetsPath + "/lol.json");
        }
    }
}