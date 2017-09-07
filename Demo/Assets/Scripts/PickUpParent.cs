using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public Transform sphere;
    

    //Awake constantly runs
    //Start only runs after it is init

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

	}

	//Gets updated frame data every 50 frames
    //Change to Update if we are missing data
    //Udpate has a different refresh but is more cpu intensive
    
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Touch' on the Trigger");
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Down' on the Trigger");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'UP' the Trigger");
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Touch' on the Trigger");
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Down' on the Trigger");
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'UP' the Trigger");
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("You are using the touchpad");
            sphere.transform.position = new Vector3(0, 0, 0);
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
             
        }
    }

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("you have collided with " + col.name + "and activted OnTriggerStay");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + "while holding down touch");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(gameObject.transform);

        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released Tough while colliding with" + col.name);
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;


            //good practice to check on the rigidbody
            tossObject(col.attachedRigidbody);
        }
    }

    private void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.velocity = device.angularVelocity;
        }
    }

}
