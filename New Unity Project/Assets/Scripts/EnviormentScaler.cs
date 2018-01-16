using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentScaler : MonoBehaviour {
    //gets the tracking of controller
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
	}
}
