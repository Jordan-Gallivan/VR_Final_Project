using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour, IComparable
{
    private Vector3 nodePos;

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
    public string Name => nodeName.ToString();

    // Start is called before the first frame update
    void Start()
    {
        nodePos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CompareTo(object other)
    {
        if (other.GetType() != this.GetType()) return 1;
        Node otherNode = (Node)other;
        if (otherNode.Name == this.Name) return 0;
        else return -1;
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
    
}
