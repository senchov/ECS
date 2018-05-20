using System.Collections.Generic;
using System.IO;
using Graphs;
using NUnit.Framework;
using SimpleJSON;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using UnityEngine.TestTools;

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
                graph.AddNode(new NavGraphNode(i, Vector2.down, ExtraInfoEnum.None));
            }

            Assert.AreEqual(nodeQuntity, graph.GetNodesQuantity());
        }

        [Test]
        public void TestAddEdge()
        {
            Graph<NavGraphNode, GraphEdge> graph = new Graph<NavGraphNode, GraphEdge>();
            int[] edgeNodes = {5, 7, 8};
            int node = 5;
            for (int i = 0; i < edgeNodes.Length; i++)
            {
                graph.AddEdge(new GraphEdge(5, edgeNodes[i]), node);
            }

            GraphEdge expectEdge = new GraphEdge(node, edgeNodes[2]);
            GraphEdge actualEdge = graph.GetNodeEdges(node)[2];
            Assert.AreEqual(expectEdge.To, actualEdge.To);
            Assert.AreEqual(expectEdge.From, actualEdge.From);
        }

        [Test]
        public void SaveFileTest()
        {
            Graph<NavGraphNode, GraphEdge> graph = new Graph<NavGraphNode, GraphEdge>();
            AddNodesAndEdgesToGraph(graph);

            string path = Application.streamingAssetsPath + "/testSaveFile.txt";
            graph.SaveToFile(path);
            Graph<NavGraphNode, GraphEdge> loadedGraph = Graph<NavGraphNode, GraphEdge>.LoadGraphFromFile(path);

            Assert.AreEqual(graph.GetNodesQuantity(), loadedGraph.GetNodesQuantity());

            for (int i = 0; i < graph.GetNodesQuantity(); i++)
            {
                CheckNodes(graph, loadedGraph, i);
                CheckEdges(graph, loadedGraph, i);
            }

            File.Delete(path);
        }

        private void CheckNodes(Graph<NavGraphNode, GraphEdge> graph, Graph<NavGraphNode, GraphEdge> loadedGraph,
            int i)
        {
            NavGraphNode expectedNode = graph.GetNode(i);
            NavGraphNode actualNode = loadedGraph.GetNode(i);
            Assert.AreEqual(expectedNode.Position, actualNode.Position);
            Assert.AreEqual(expectedNode.ExtraInfo, actualNode.ExtraInfo);
        }

        private void CheckEdges(Graph<NavGraphNode, GraphEdge> graph, Graph<NavGraphNode, GraphEdge> loadedGraph,
            int i)
        {
            List<GraphEdge> expectEdges = graph.GetNodeEdges(i);
            List<GraphEdge> actualEdges = loadedGraph.GetNodeEdges(i);
            Assert.AreEqual(expectEdges.Count, actualEdges.Count);

            CompareEachEdgeInLists(expectEdges, actualEdges);
        }

        private void CompareEachEdgeInLists(List<GraphEdge> expectEdges, List<GraphEdge> actualEdges)
        {
            using (IEnumerator<GraphEdge> expectEdgesEnumerator = expectEdges.GetEnumerator(),
                actualEdgesEnumerator = actualEdges.GetEnumerator())
            {
                while (expectEdgesEnumerator.MoveNext() && actualEdgesEnumerator.MoveNext())
                {
                    GraphEdge expectedEdge = expectEdgesEnumerator.Current;
                    GraphEdge actualEdge = actualEdgesEnumerator.Current;
                    Assert.AreEqual(expectedEdge.From, actualEdge.From);
                    Assert.AreEqual(expectedEdge.To, actualEdge.To);
                    Assert.AreEqual(expectedEdge.Cost, actualEdge.Cost);
                }
            }
        }

        private void AddNodesAndEdgesToGraph(Graph<NavGraphNode, GraphEdge> graph)
        {
            graph.AddNode(new NavGraphNode(0, new Vector2(80, 58.8f), ExtraInfoEnum.None));
            graph.AddNode(new NavGraphNode(0, new Vector2(95, 58.8f), ExtraInfoEnum.None));
            graph.AddNode(new NavGraphNode(0, new Vector2(65, 58.8f), ExtraInfoEnum.None));
            graph.AddNode(new NavGraphNode(0, new Vector2(80, 43.8f), ExtraInfoEnum.None));
            graph.AddNode(new NavGraphNode(0, new Vector2(80, 73.8f), ExtraInfoEnum.None));

            graph.AddEdge(new GraphEdge(0, 1, 15), 0);
            graph.AddEdge(new GraphEdge(0, 2, 15), 0);
            graph.AddEdge(new GraphEdge(0, 3, 15), 0);
            graph.AddEdge(new GraphEdge(0, 4, 15), 0);

            graph.AddEdge(new GraphEdge(1, 0, 15), 1);
            graph.AddEdge(new GraphEdge(1, 3, 21.2f), 1);
            graph.AddEdge(new GraphEdge(1, 4, 21.2f), 1);

            graph.AddEdge(new GraphEdge(2, 0, 15), 2);
            graph.AddEdge(new GraphEdge(2, 4, 21.2f), 2);
            graph.AddEdge(new GraphEdge(2, 3, 21.2f), 2);

            graph.AddEdge(new GraphEdge(3, 0, 15), 3);
            graph.AddEdge(new GraphEdge(3, 1, 21.2f), 3);
            graph.AddEdge(new GraphEdge(3, 2, 21.2f), 3);

            graph.AddEdge(new GraphEdge(4, 0, 15), 4);
            graph.AddEdge(new GraphEdge(4, 1, 21.2f), 4);
            graph.AddEdge(new GraphEdge(4, 2, 21.2f), 4);
        }
    }
}