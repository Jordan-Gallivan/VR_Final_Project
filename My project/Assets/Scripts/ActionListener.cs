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
    [SerializeField] private InputActionReference leftTrig;
    [SerializeField] private InputActionReference rightTrig;
    [SerializeField] private InputActionReference leftGrasp;
    [SerializeField] private InputActionReference rightGrasp;
    [SerializeField] private InputActionReference leftTrackPad;
    [SerializeField] private InputActionReference rightTrackPad;
    [SerializeField] private InputActionReference leftControllerPos;
    [SerializeField] private InputActionReference leftControllerRotation;

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
        
        itemIsMoving = false;
        itemIsRotating = false;
        origControllerPos = leftControllerPos.action.ReadValue<Vector3>();
        updatedControllerPos = origControllerPos;
        origControllerRot = leftControllerRotation.action.ReadValue<Quaternion>();
        updatedControllerRot = origControllerRot;
    }

    // Update is called once per frame
    void Update()
    {
        updatedControllerPos = leftControllerPos.action.ReadValue<Vector3>();
        updatedControllerRot = leftControllerRotation.action.ReadValue<Quaternion>();

        /////////////////////////////////////////
        /////   Detect controller inputs    /////
        /////////////////////////////////////////

        // left Grasp => 
        if (leftGrasp.action.WasPerformedThisFrame())
        {
            Debug.Log("Left Grasp Works");
            // leftgrasp functionality
        }
        if (leftGrasp.action.WasReleasedThisFrame())
        {
            Debug.Log("Left Grasp Deselected");
            // left grasp deactivate
        }

        // left Trig => 
        if (leftTrig.action.WasPerformedThisFrame())
        {
            Debug.Log("Left Trig Works");
            // left trig
        }
        if (leftTrig.action.WasReleasedThisFrame())
        {
            Debug.Log("Left Trig deselected");
            // left trig 
        }
        
        // right grasp => Move and Rotate Object
        if (rightGrasp.action.WasPerformedThisFrame())
        {
            Debug.Log("Right Grasp Works");
            // itemIsMoving = true;
            // itemIsRotating = true;
        }
        if (rightGrasp.action.WasReleasedThisFrame())
        {
            Debug.Log("Right Grasp Deselected");
            // tractorBeam.EndMovement();
            // tractorBeam.EndRotation();
            // itemIsMoving = false;
            // itemIsRotating = false;
        }
        
        // right trigger => Summon Object
        if (rightTrig.action.WasPerformedThisFrame())
        {
            Debug.Log("Right Trig Works");
            // right trig
        }
        if (rightTrig.action.WasReleasedThisFrame())
        {
            Debug.Log("Right Trig deselected");
            // right trig
        }
        
        /////////////////////////////////////////
        /////   Update Player and Item      /////
        /////////////////////////////////////////

        // move and rotate the player according to trackpad input
        player.MovePlayer(leftTrackPad.action.ReadValue<Vector2>());
        player.RotatePlayer(rightTrackPad.action.ReadValue<Vector2>());
        
        // move and rotate selected item
        // if (itemIsMoving && itemIsRotating)
        // {
        //     tractorBeam.MoveItem(updatedControllerPos - origControllerPos);
        //     tractorBeam.RotateItem(updatedControllerRot.eulerAngles - origControllerRot.eulerAngles);
        // }

        origControllerPos = updatedControllerPos;
        origControllerRot = updatedControllerRot;
    }

}
