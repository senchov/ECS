using System.Collections;
using System.Collections.Generic;
using Graphs;
using UnityEngine;

public class NavGraphCreator : MonoBehaviour
{
    [SerializeField] private Transform StartPoint;
    [SerializeField] private float MaxDistanceToNode;

    private Graph<NavGraphNode, GraphEdge> Graph;

    [ContextMenu("CreateGraph")]
    public void CreateGraph()
    {
        Graph = new Graph<NavGraphNode, GraphEdge>();
        
    }
}