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

    public SteamVR_Action_Boolean menu;
    public SteamVR_Action_Boolean grasp;
    public SteamVR_Action_Boolean trig;
    public SteamVR_Action_Boolean touchPadSelect;
    public SteamVR_Action_Vector2 touchPad;


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
        
        
        if (grasp.GetState(SteamVR_Input_Sources.LeftHand)) {
            LeftGrasp();
        }

        /////////////////////////////////////////
        /////   Update Player and Item      /////
        /////////////////////////////////////////

        // move and rotate the player according to trackpad input
        Debug.Log(touchPad.GetAxis(SteamVR_Input_Sources.LeftHand));
        Debug.Log(touchPad.GetAxis(SteamVR_Input_Sources.RightHand));
        player.MovePlayer(touchPad.GetAxis(SteamVR_Input_Sources.LeftHand));
        player.RotatePlayer(touchPad.GetAxis(SteamVR_Input_Sources.RightHand));

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
