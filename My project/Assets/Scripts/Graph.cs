using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private HashSet<Node> nodes;
    private Dictionary<Node, HashSet<Edge>> edges;

    protected Graph()
    {
        nodes = new HashSet<Node>();
        edges = new Dictionary<Node, HashSet<Edge>>();
    }

    public bool AddNode(Node node)
    {
        if (nodes.Contains(node)) return false;
        nodes.Add(node);
        edges.Add(node, new HashSet<Edge>());
        return true;
    }

    public bool AddEdge(Edge edge)
    {
        Node src = edge.Source;
        Node dest = edge.Destination;
        if (!nodes.Contains(src)) AddNode(src);
        if (!nodes.Contains(dest)) AddNode(dest);
        if (!edges[src].Contains(edge)) edges[src].Add(edge);
        if (!edges[dest].Contains(edge)) edges[dest].Add(edge);
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            foreach (var node in nodes)
            {
                foreach (var edge in edges[node])
                {
                    Debug.Log(edge.ToString());
                }
            }
        }
    }
}
