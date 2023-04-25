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
using Valve.VR;



public class ActionListener : MonoBehaviour
{
    // Player Objects
    [SerializeField] private GameObject playerGO;
    private PlayerScript player;
    
    // HUD Game Object and Script
    [SerializeField] private GameObject HUDGO;
    private HUD HUDScript;

    // [SerializeField] private SteamVR_Action_Single menu;
    // [SerializeField] private SteamVR_Action_Vector2 touchPad;


    
    // inputs
    // [SerializeField] private InputActionReference leftTrig;
    // [SerializeField] private InputActionReference rightTrig;
    // [SerializeField] private InputActionReference leftGrasp;
    // [SerializeField] private InputActionReference rightGrasp;
    // [SerializeField] private InputActionReference leftTrackPad;
    // [SerializeField] private InputActionReference rightTrackPad;
    // [SerializeField] private InputActionReference leftTPSelect;
    // [SerializeField] private InputActionReference rightTPSelect;


    // // Boolean Control values
    // private bool itemIsMoving;
    // private bool itemIsRotating;
    
    // // controller positions
    // private Vector3 origControllerPos = Vector3.zero;
    // private Vector3 updatedControllerPos = Vector3.zero;
    // private Quaternion origControllerRot = Quaternion.identity;
    // private Quaternion updatedControllerRot = Quaternion.identity;

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

        // if (SteamVR_Input.default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any)) {
        //     LeftTPPress();
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

    public void LeftMenu() {
        Debug.Log("Left Menu");
    }

    public void RightMenu() {
        Debug.Log("Right Menu");
    }


}
