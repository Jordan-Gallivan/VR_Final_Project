using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour, IComparable
{
    [SerializeField] private GameObject graph;
    private Arrows edgeArrows;
    [SerializeField] private GameObject source;
    private Node start;
    public Node Source => start;
    [SerializeField] private GameObject destination;
    private Node dest;
    public Node Destination => dest;
    private float weight;
    public float Weight => weight;

    private void Start()
    {
        start = source.GetComponent<Node>();
        dest = destination.GetComponent<Node>();
        weight = Vector3.Distance(source.transform.position, destination.transform.position);
        
        graph.GetComponent<Graph>().AddEdge(this);
        
        edgeArrows = this.GetComponent<Arrows>();
    }

    

    public void ActivateArrows()
    {
        edgeArrows.ArrowsActivated = true;
    }

    public void DeactivateArrows()
    {
        edgeArrows.ArrowsActivated = false;
    }

    public int CompareTo(object other)
    {
        if (other.GetType() != this.GetType()) return 1;
        Edge otherEdge = (Edge)other;
        if (otherEdge.Source == start && otherEdge.Destination == dest) return 0;
        return -1;
    }
    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }
    public override string ToString()
    {
        return $"[{start.Name}] <-> [{dest.Name}] ({weight})";
    }
}
