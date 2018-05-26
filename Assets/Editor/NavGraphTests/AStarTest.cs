using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace Graphs
{
    public class AStarTest
    {
        private AStar AStar;
        private Graph<NavGraphNode, GraphEdge> Graph;

        [Test]
        public void GetPathTest()
        {
            Graph = new Graph<NavGraphNode, GraphEdge>();
            AddFakeGraphNodes(Graph);
            AddFakeGraphEdges(Graph);

            CheckGetPath(new int[] {0, 5, 10, 15}, 0, 15);
            CheckGetPath(new int[] {0, 4, 8, 12}, 0, 12);
            CheckGetPath(new int[] {12, 9, 6, 3}, 12, 3);
            CheckGetPath(new int[] {4, 5, 6, 7}, 4, 7);
            CheckGetPath(new int[] {2, 6, 10, 14}, 2, 14);
            CheckGetPath(new int[] {2, 6}, 2, 6);
            CheckGetPath(new int[] {13, 10, 7}, 13, 7);
        }

        private void CheckGetPath(int[] expectedPath, int startIndex, int goalIndex)
        {
            AStar = new AStar(Graph);
            int[] path = AStar.GetPath(startIndex, goalIndex);
            for (int i = 0; i < path.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], path[i]);
            }
        }

        private void AddFakeGraphEdges(Graph<NavGraphNode, GraphEdge> graph)
        {
            int nodeQuntity = graph.GetNodesQuantity();
            for (int i = 0; i < nodeQuntity; i++)
            {
                NavGraphNode outerNode = graph.GetNode(i);
                for (int j = 0; j < nodeQuntity; j++)
                {
                    NavGraphNode innerNode = graph.GetNode(j);
                    if (IsNodeConected(outerNode, innerNode))
                    {
                        GraphEdge edge = new GraphEdge(outerNode.Index, innerNode.Index, 0);
                        graph.AddEdge(edge, outerNode.Index);
                    }
                }

                //DebugConnectedNodes(graph, outerNode);
            }
        }

        private static bool IsNodeConected(NavGraphNode first, NavGraphNode second)
        {
            return first.Index != second.Index && Vector2.Distance(second.Position, first.Position) < 14.5f;
        }

        private void AddFakeGraphNodes(Graph<NavGraphNode, GraphEdge> graph)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = i * 4 + j;
                    Vector2 pos = new Vector2(j * 10, i * 10);
                    graph.AddNode(new NavGraphNode(index, pos));
                }
            }
        }

        private static void DebugConnectedNodes(Graph<NavGraphNode, GraphEdge> graph, NavGraphNode outerNode)
        {
            string connectedNodesText = outerNode.Index + " " + outerNode.Position + "->";
            foreach (int connectedNodesId in graph.GetConnectedNodesIds(outerNode.Index))
            {
                connectedNodesText += connectedNodesId + ",";
            }

            Debug.LogError(connectedNodesText);
        }
    }
}