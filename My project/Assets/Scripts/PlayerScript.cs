using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody rb;

    public GameObject player;
    public float playerHeight = 1.5f;

    public float velocityConstant = 2.0f;
    public float rotationConstant = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rayDown();
    }


    public void MovePlayer(Vector2 trackPad)
    {
        // z = forward (vector2.y)
        // x = strafe (vector2.x)
        
        rb.AddForce(trackPad.x * velocityConstant, 0f , trackPad.y * velocityConstant, 
            ForceMode.VelocityChange);
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
