using System;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class AStar
    {
        private Graph<NavGraphNode, GraphEdge> Graph;
        private List<NavGraphNode> OpenList;
        private List<NavGraphNode> CloseList;

        public AStar(Graph<NavGraphNode, GraphEdge> graph)
        {
            Graph = graph;

            OpenList = new List<NavGraphNode>();
            CloseList = new List<NavGraphNode>();
        }

        public int[] GetPath(int startNodeIndex, int goalNodeIndex)
        {
            NavGraphNode startNode = Graph.GetNode(startNodeIndex);
            NavGraphNode goalNode = Graph.GetNode(goalNodeIndex);
            OpenList.Add(startNode);
            startNode.Hcost = EstimateHeuristicCost(startNode, goalNode);
            startNode.Gcost = 0;

            NavGraphNode node = new NavGraphNode(-1);
            while (OpenList.Count > 0)
            {
                node = GetFirstFreeNode();

                if (node.Index == goalNode.Index)
                    return CalculatePath(node);

                List<NavGraphNode> connectedNodes = Graph.GetConnectedNodes(node.Index);

                foreach (NavGraphNode connectedNode in connectedNodes)
                {
                    if (!CloseList.Contains(connectedNode))
                    {
                        CalculateCostsForNode(node, connectedNode, goalNode);

                        if (!OpenList.Contains(connectedNode))
                        {
                            AddNodeAndSortList(connectedNode, OpenList);
                        }
                    }
                }

                AddNodeAndSortList(node, CloseList);
                RemoveAndSortList(node, OpenList);
            }

            if (node.Index != goalNode.Index)
            {
                Debug.LogError("goal no found");
                return null;
            }

            return CalculatePath(node);
        }

        private float EstimateHeuristicCost(NavGraphNode currentNode, NavGraphNode goalNode)
        {
            return (currentNode.Position - goalNode.Position).magnitude;
        }

        private NavGraphNode GetFirstFreeNode()
        {
            return OpenList[0];
        }
        
        private int[] CalculatePath(NavGraphNode node)
        {
            List<int> path = new List<int>();
            
            while (node != null)
            {
                int nodeIndex = node.Index;
                path.Add(nodeIndex);
                node = node.Parent;
            }

            ClearOpenAndCloseLists();
            path.Reverse();
            return path.ToArray();
        }

        private void CalculateCostsForNode(NavGraphNode node, NavGraphNode connectedNode, NavGraphNode goalNode)
        {
            float cost = EstimateHeuristicCost(node, connectedNode);
            float totalCost = node.Gcost + cost;
            float connectedToGoalCost = EstimateHeuristicCost(connectedNode, goalNode);
            connectedNode.Gcost = totalCost;
            connectedNode.Parent = node;
            connectedNode.Hcost = totalCost + connectedToGoalCost;
        }
        
        private void AddNodeAndSortList(NavGraphNode connectedNode, List<NavGraphNode> nodeList)
        {
            nodeList.Add(connectedNode);
            nodeList.Sort();
        }

        private void RemoveAndSortList(NavGraphNode node, List<NavGraphNode> nodeList)
        {
            OpenList.Remove(node);
            OpenList.Sort();
        }

        private void ClearOpenAndCloseLists()
        {
            SetAllParentNodesToNullAndClearList(OpenList);
            SetAllParentNodesToNullAndClearList(CloseList);
        }

        private void SetAllParentNodesToNullAndClearList(List<NavGraphNode> list)
        {
            foreach (NavGraphNode navGraphNode in list)
            {
                navGraphNode.Parent = null;
            }
            list.Clear();
        }
    }
}