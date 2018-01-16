using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentScaler : MonoBehaviour {
    //gets the tracking of controller
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;
    //tells if grips are pressed
    public bool pressed;
	// Use this for initialization
	void Start () {
        trackedObj = gameObject.GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObj.index);
        pressed = device.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public bool isPressed()
    {
       return pressed;
    }

}
