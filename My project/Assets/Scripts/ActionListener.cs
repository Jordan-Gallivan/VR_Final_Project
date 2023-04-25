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
        
        
        if (menu.GetChanged(SteamVR_Input_Sources.LeftHand) && 
            menu.GetState(SteamVR_Input_Sources.LeftHand)) LeftMenu();
        if (menu.GetChanged(SteamVR_Input_Sources.RightHand) && 
            menu.GetState(SteamVR_Input_Sources.RightHand)) RightMenu();
        if (touchPadSelect.GetChanged(SteamVR_Input_Sources.LeftHand) && 
            touchPadSelect.GetState(SteamVR_Input_Sources.LeftHand)) LeftTPPress();
        

        /////////////////////////////////////////
        /////   Update Player and Item      /////
        /////////////////////////////////////////
        Vector2 leftTP = touchPad.GetAxis(SteamVR_Input_Sources.LeftHand);
        Vector2 rightTP = touchPad.GetAxis(SteamVR_Input_Sources.RightHand);
        
        
        if (HUDScript.HUDActive) return;
        
        // move and rotate the player according to trackpad input
        Debug.Log(leftTP);
        Debug.Log(rightTP);
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
        Vector2 leftTP = touchPad.GetAxis(SteamVR_Input_Sources.LeftHand);
        float theta = Mathf.Atan(leftTP.y / leftTP.x);
        float r = Mathf.Sqrt(Mathf.Pow(leftTP.y, 2.0f) + Mathf.Pow(leftTP.x, 2.0f));
        if (!HUDScript.HUDActive) return;
        Debug.Log(theta);
        Debug.Log(r);
        if (r < 0.5) Debug.Log("Center");
        else {
            if (leftTP.y > 0 && (theta > Mathf.PI / 4f || theta < -Mathf.PI / 4f)) Debug.Log("Swipe Up");
            else if (leftTP.y < 0 && (theta > Mathf.PI / 4f || theta < -Mathf.PI / 4f)) Debug.Log("Swipe Down");
            else if (leftTP.x > 0) Debug.Log("Swipe Right");
            else Debug.Log("Swipe Left");
        }
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
        if (HUDScript.HUDActive)HUDScript.SwipeLeft();
        else HUDScript.ActivateHUD();
    }

    public void RightMenu() {
        if(HUDScript.NavigatingToExhibit) HUDScript.EndNavigation();
        Debug.Log("Right Menu");
    }


}
