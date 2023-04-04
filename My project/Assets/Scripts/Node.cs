using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour, IComparable
{
    // Enumerable Object of available node names for user selection
    enum Names
    {
        A1,
        A2,
        A3,
        B1,
        B2,
        B3,
    };
    [SerializeField] private Names nodeName = new Names();
    public string Name => nodeName.ToString();  // getter for Node Name
    
    // Node color and render properties
    private Renderer nodeRenderer;
    private Color clear = new Color(1f, 1f, 1f, 0f);
    private Color green = new Color(1f, 1f, 1f, 1f);
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    
    void Start()
    {
        nodeRenderer = this.gameObject.GetComponent<Renderer>();
        MakeNodeClear();
    }
    
    /// <summary>
    /// Highlights the current node, called when node is along path
    /// </summary>
    public void HighLightNode()
    {
        nodeRenderer.material.SetColor(Color1, green);
    }
    
    /// <summary>
    /// Deactivates current node, makes transpartent
    /// </summary>
    public void MakeNodeClear()
    {
        nodeRenderer.material.SetColor(Color1, clear);
    }
    
    /// <summary>
    /// Overrides CompareTo.  Compares name of Nodes being compared
    /// </summary>
    /// <param name="other">Object to be compared</param>
    /// <returns>0 if this.Name = other.Name; 1 if other is not of type Node,
    /// -1 otherwise</returns>
    public int CompareTo(object other)
    {
        if (other.GetType() != this.GetType()) return 1;
        Node otherNode = (Node)other;
        if (otherNode.Name == this.Name) return 0;
        else return -1;
    }
    /// <summary>
    /// Overrides GetHashCode.
    /// </summary>
    /// <returns>Hashcode of node.Name</returns>
    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
    
}
