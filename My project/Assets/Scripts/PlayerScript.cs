using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Player Objects
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject player;
    
    // Player settings
    [SerializeField] private float playerHeight;
    [SerializeField] private float velocityConstant = 2.0f;
    [SerializeField] private float rotationConstant = 1f;
    
    // Nearest Nodes
    private Node nearestNode;
    private GameObject nearestNodeGO;
    public Node NearestNode => nearestNode;
    
    private Node nearestMusicNode;
    private GameObject nearestMusicNodeGO;
    public Node NearestMusicNode => nearestMusicNode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayDown();

        // Determine Nearest Nodes
        RaycastHit[] nodes = Physics.SphereCastAll(
            new Vector3(player.transform.position.x, 
                player.transform.position.y + 20f, player.transform.position.z),
            10.0f, new Vector3(0f, -1f, 0f));
        float nearestNodeDist = Mathf.Infinity;
        float nearestMusicNodeDist = Mathf.Infinity;

        nearestNode = null;
        nearestMusicNode = null;
        nearestNodeGO = null;
        var prevMusicNodeGO = nearestMusicNodeGO;
        nearestMusicNodeGO = null;
        
        foreach (RaycastHit hit in nodes)
        {
            // determine nearest node for navigation
            if ((hit.distance < nearestNodeDist) && (hit.collider.gameObject != nearestNodeGO) &&
                hit.transform.gameObject.layer == LayerMask.NameToLayer("Node"))
            {
                nearestNodeDist = hit.distance;
                nearestNodeGO = hit.transform.gameObject;
            }
            
            // determine nearest node for music
            if ((hit.distance < nearestMusicNodeDist) && (hit.collider.gameObject != nearestMusicNodeGO) &&
                hit.transform.gameObject.layer == LayerMask.NameToLayer("MusicNode"))
            {
                nearestMusicNodeDist = hit.distance;
                nearestMusicNodeGO = hit.transform.gameObject;
            }
        }

        if (nearestNodeGO != null) nearestNode = nearestNodeGO.GetComponent<Node>();

        if (nearestMusicNodeGO != null && nearestMusicNodeGO != prevMusicNodeGO)
        {
            // Deactivate previous music
            // Activate new music
        }
        
        
    }


    public void MovePlayer(Vector2 trackPad)
    {
        // add raycast in direction of trackpad, only excute movement if that raycast is clean

        // z = forward (vector2.y)
        // x = strafe (vector2.x)
        if ((trackPad.x > -0.1 && trackPad.x < 0.1) && (trackPad.y > -0.1 && trackPad.y < 0.1)) 
            rb.AddRelativeForce(0f, 0f , 0f, ForceMode.VelocityChange);
        else 
            rb.AddRelativeForce(trackPad.x * velocityConstant, 0f , 
                trackPad.y * velocityConstant, ForceMode.VelocityChange);
        
        
        
    }

    public void RotatePlayer(Vector2 trackPad)
    {
        // - vector2.x == + rotation about y axis
        if (!(trackPad.x > -0.2 && trackPad.x < 0.2))
        {
           player.transform.Rotate(new Vector3(0f, trackPad.x * rotationConstant, 0f), Space.Self);
        }
    }

    public void rayDown()
    {
        RaycastHit hit;
        Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.down), out hit, 100f);
        player.transform.position = new Vector3(player.transform.position.x, hit.point.y + playerHeight, player.transform.position.z);
        
    }
}
