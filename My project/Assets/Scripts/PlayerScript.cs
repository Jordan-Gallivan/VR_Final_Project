using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GraphGO;

    public Node testNodeA;
    public Node testNodeB;
    
    public Node testNodeC;
    public Node testNodeD;

    private Graph graphObj;
    
    
    // Start is called before the first frame update
    void Start()
    {
        graphObj = GraphGO.GetComponent<Graph>();
        
        Invoke("CallDisplayPath", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDisplayPath()
    {
        Node nearestNode;
        
        graphObj.DisplayPath(testNodeC,testNodeD);
    }
}
