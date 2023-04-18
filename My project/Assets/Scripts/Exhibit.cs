using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhibit : MonoBehaviour
{
    [SerializeField] private GameObject nearestNodeGO;

    private Node nearestNode;
    public Node NearestNode => nearestNode;
    
    // Start is called before the first frame update
    void Start()
    {
        nearestNode = nearestNodeGO.GetComponent<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
