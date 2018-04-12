using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour {
    public DrawLineManager manager;
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device dev;
    private SteamVR_TrackedController controller;
    bool toggle;

    

    // Use this for initialization
    void Start () {
        if (!manager.toggle)
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
            controller = GetComponent<SteamVR_TrackedController>();
            controller.PadClicked += Controller_PadClicked;
   
        }
	}

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
        if(dev.GetAxis().x <= 0)
        {
            return;
           // Destroy(manager.go);
        }
    }
  
    // Update is called once per frame
    void Update () {
        /*
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !manager.toggle)
        {
            //Debug.LogError("got here");
           




        }
      */
		
	}

}
