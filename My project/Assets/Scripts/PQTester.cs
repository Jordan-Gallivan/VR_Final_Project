using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PQTester : MonoBehaviour
{
    public Node n1;

    public Node n2;

    public Node n3;

    public Node n4;
    // Start is called before the first frame update
    void Start()
    {
        DJPriorityQueue pq = new DJPriorityQueue();
        pq.Add(new QueueElement(n1, new List<Edge>(), 10f));
        pq.Add(new QueueElement(n2, new List<Edge>(), 15f));
        pq.Add(new QueueElement(n3, new List<Edge>(), 5f));
        pq.Add(new QueueElement(n4, new List<Edge>(), 20f));

        while (!pq.IsEmpty())
        {
            var qe = pq.Pop();
            Debug.Log(qe.DestNode.ToString());
            Debug.Log( qe.Distance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
