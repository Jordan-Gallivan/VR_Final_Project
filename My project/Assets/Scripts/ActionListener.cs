using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR;



public class ActionListener : MonoBehaviour
{
    // Player Objects
    [SerializeField] private GameObject playerGO;
    private PlayerScript player;
    
    // HUD Game Object and Script
    [SerializeField] private GameObject HUDGO;
    private HUD HUDScript;
    
    // inputs
    // [SerializeField] private InputActionReference leftTrig;
    // [SerializeField] private InputActionReference rightTrig;
    // [SerializeField] private InputActionReference leftGrasp;
    // [SerializeField] private InputActionReference rightGrasp;
    // [SerializeField] private InputActionReference leftTrackPad;
    // [SerializeField] private InputActionReference rightTrackPad;
    // [SerializeField] private InputActionReference leftTPSelect;
    // [SerializeField] private InputActionReference rightTPSelect;


    // Boolean Control values
    private bool itemIsMoving;
    private bool itemIsRotating;
    
    // controller positions
    private Vector3 origControllerPos = Vector3.zero;
    private Vector3 updatedControllerPos = Vector3.zero;
    private Quaternion origControllerRot = Quaternion.identity;
    private Quaternion updatedControllerRot = Quaternion.identity;

    void Start()
    {
        player = playerGO.GetComponent<PlayerScript>();
        HUDScript = HUDGO.GetComponent<HUD>();
        //
        // itemIsMoving = false;
        // itemIsRotating = false;
        // origControllerPos = leftControllerPos.action.ReadValue<Vector3>();
        // updatedControllerPos = origControllerPos;
        // origControllerRot = leftControllerRotation.action.ReadValue<Quaternion>();
        // updatedControllerRot = origControllerRot;
    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////////////////////////
        /////   Detect controller inputs    /////
        /////////////////////////////////////////
    
        // left Grasp => 
        // if (leftGrasp.action.WasPerformedThisFrame())
        // {
        //     Debug.Log("Left Grasp Works");
        //     // leftgrasp functionality
        // }
        // if (leftGrasp.action.WasReleasedThisFrame())
        // {
        //     Debug.Log("Left Grasp Deselected");
        //     // left grasp deactivate
        // }
        //
        // // left Trig => 
        // if (leftTrig.action.WasPerformedThisFrame())
        // {
        //     Debug.Log("Left Trig Works");
        //     // left trig
        // }
        // if (leftTrig.action.WasReleasedThisFrame())
        // {
        //     Debug.Log("Left Trig deselected");
        //     // left trig 
        // }
        //
        // // right grasp => Move and Rotate Object
        // if (rightGrasp.action.WasPerformedThisFrame())
        // {
        //     Debug.Log("Right Grasp Works");
        //     // itemIsMoving = true;
        //     // itemIsRotating = true;
        // }
        // if (rightGrasp.action.WasReleasedThisFrame())
        // {
        //     Debug.Log("Right Grasp Deselected");
        //     // tractorBeam.EndMovement();
        //     // tractorBeam.EndRotation();
        //     // itemIsMoving = false;
        //     // itemIsRotating = false;
        // }
        //
        // // right trigger => Summon Object
        // if (rightTrig.action.WasPerformedThisFrame())
        // {
        //     Debug.Log("Right Trig Works");
        //     // right trig
        // }
        // if (rightTrig.action.WasReleasedThisFrame())
        // {
        //     Debug.Log("Right Trig deselected");
        //     // right trig
        // }
        //
        // if (leftTPSelect.action.WasPerformedThisFrame())
        // {
        //     Debug.Log("Left TP Select Works");
        //     // right trig
        // }
        // if (leftTPSelect.action.WasReleasedThisFrame())
        // {
        //     Debug.Log("Left TP deselected");
        //     // right trig
        // }
        //
        // if (rightTPSelect.action.WasPerformedThisFrame())
        // {
        //     Debug.Log("Right TP Select Works");
        //     // right trig
        // }
        // if (rightTPSelect.action.WasReleasedThisFrame())
        // {
        //     Debug.Log("Right TP deselected");
        //     // right trig
        // }
        
        /////////////////////////////////////////
        /////   Update Player and Item      /////
        /////////////////////////////////////////
    
        // move and rotate the player according to trackpad input
        // player.MovePlayer(leftTrackPad.action.ReadValue<Vector2>());
        // player.RotatePlayer(rightTrackPad.action.ReadValue<Vector2>());
        
        // move and rotate selected item
        // if (itemIsMoving && itemIsRotating)
        // {
        //     tractorBeam.MoveItem(updatedControllerPos - origControllerPos);
        //     tractorBeam.RotateItem(updatedControllerRot.eulerAngles - origControllerRot.eulerAngles);
        // }
    }

    public void LeftGrasp()
    {
        Debug.Log("Left Grasp");
    }

    public void RightGrasp()
    {
        Debug.Log("Right Grasp");
    }

    public void LeftTrig()
    {
        Debug.Log("Left Trig");
    }

    public void RightTrig()
    {
        Debug.Log("Right Trig");
    }

    public void LeftTPPress()
    {
        Debug.Log("Left TP Press");
    }

    public void RightTPPress()
    {
        Debug.Log("Right TP Press");
    }

    public void LeftTP()
    {
        Debug.Log("Left TP");
    }

    public void RightTP()
    {
        Debug.Log("Right TP");
    }

}
