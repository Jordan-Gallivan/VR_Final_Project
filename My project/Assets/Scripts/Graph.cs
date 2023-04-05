using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // All nodes in graph
    [SerializeField] private GameObject nodeEmpty;
    // All edges in graph
    [SerializeField] private GameObject edgeEmpty;
    [SerializeField] private GameObject edgeEmptyClone;

    [SerializeField] private float buildTimeDelay;
    
    // internal storage of nodes and edges
    //  nodes stored in Hash Set
    //  edges stored in Dictionary {Node : {edge1, edge2, ...} }
    private HashSet<Node> nodes;
    private Dictionary<Node, HashSet<Edge>> edges;

    private Dictionary<Node, Dictionary<Node, QueueElement>> shortestPaths;


    public void Start()
    {
        Console.WriteLine("test");
        // initialize nodes and edges
        nodes = new HashSet<Node>();
        edges = new Dictionary<Node, HashSet<Edge>>();
        shortestPaths = new Dictionary<Node, Dictionary<Node, QueueElement>>();
        
        // delays building of the graph by buildTimeDelay seconds
        // Invoke("BuildGraph", buildTimeDelay);
        
        BuildGraph();
        BuildShortestPaths();
        DJTest();
        
    }
    
    /// <summary>
    /// Adds all Nodes and Edges in Scene to nodes and edges
    /// </summary>
    private void BuildGraph()
    {
        foreach (Transform child in nodeEmpty.transform)
        {
            Node n = child.gameObject.GetComponent<Node>();
            nodes.Add(n);
            edges.Add(n, new HashSet<Edge>());
        }
        
        // iterate through all edges and add to edges.
        //  graph is Not directional and therefore each edge is added
        //  for both source and destination nodes
        foreach (Transform child in edgeEmpty.transform)
        {
            Edge e = child.gameObject.GetComponent<Edge>();
            Node src = e.SourceNode;
            Node dest = e.DestinationNode;
            edges[src].Add(e);
            // edges[dest].Add(e); 
            Edge reverseEdge = e.BuildReverseEdge(edgeEmptyClone);
            src = reverseEdge.SourceNode;
            dest = reverseEdge.DestinationNode;
            edges[src].Add(reverseEdge);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            
        }
    }

    private void BuildShortestPaths()
    {
        DJPriorityQueue pq;
        Dictionary<Node, QueueElement> nodeDict;
        // build shortest path to all nodes for each node
        foreach (Node src in nodes)
        {
            pq = new DJPriorityQueue();
            nodeDict = new Dictionary<Node, QueueElement>();
            foreach (Node n in nodes)
            {
                if (n.CompareTo(src) == 0) 
                    nodeDict.Add(n, new QueueElement(n, new List<Edge>(), 0));
                else
                    nodeDict.Add(n, new QueueElement(n, new List<Edge>(), Mathf.Infinity));

                
            }

            pq.Add(nodeDict[src]);

            while (!pq.IsEmpty())
            {
                var qe = pq.Pop();
                var currNode = qe.DestNode;
                var currDist = qe.Distance;
                var currPath = qe.Path;
                foreach (Edge e in edges[currNode])
                {
                    var adjacentNode = e.DestinationNode;
                    if (nodeDict[adjacentNode].Distance > currDist + e.Weight)
                    {
                        var newPath = currPath.GetRange(0, currPath.Count);
                        newPath.Add(e);
                        nodeDict[adjacentNode].Distance = currDist + e.Weight;
                        nodeDict[adjacentNode].Path = newPath;
                        pq.Add(nodeDict[adjacentNode]);
                    }
                }
            }
            shortestPaths.Add(src, nodeDict);
        }
    }

    public void DJTest()
    {
        // StringBuilder sb;
        // foreach (var(src, sp) in shortestPaths)
        // {
        //     Debug.Log(src.ToString());
        //     Debug.Log("------------------------");
        //     
        //     foreach (var (dest, qe) in sp)
        //     {
        //         sb = new StringBuilder();
        //         sb.Append(src.ToString());
        //         foreach (Edge e in qe.Path)
        //         {
        //             sb.Append(" -> ");
        //             sb.Append(e.DestinationNode.ToString());
        //         }
        //
        //         Debug.Log(sb.ToString());
        //     }
        //     Debug.Log("");
        // }

        StreamWriter write = new StreamWriter("Assets/Scripts/pathTest.txt", false);
        foreach (var (src, sp) in shortestPaths)
        {
            write.WriteLine(src.ToString());
            write.WriteLine("------------------------");
            foreach (var (dest, qe) in sp)
            {
                write.Write($"{src}");
                foreach (Edge e in qe.Path)
                {
                    write.Write($" -> {e.DestinationNode}");
                }
                write.WriteLine();
            }
            write.WriteLine();
        }
        write.Close();
    }
}
