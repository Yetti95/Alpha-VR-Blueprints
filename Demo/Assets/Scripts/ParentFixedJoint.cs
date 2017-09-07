﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ParentFixedJoint : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    FixedJoint fixedJoint;
    public Rigidbody rigidBodyAttachPoint;
    public Transform sphere;

    // Use this for initialization
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("You are using the touchpad");
            sphere.transform.position = new Vector3(0, 0, 0);
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        } 
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("You are using the touchpad");
        if (fixedJoint ==  null && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            fixedJoint = other.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidBodyAttachPoint;

        }
        else if (fixedJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = fixedJoint.gameObject;
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            Object.Destroy(fixedJoint);
            fixedJoint = null;
            tossObject(rigidbody);

        }
        
    }
    void tossObject(Rigidbody rigidBody)
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
