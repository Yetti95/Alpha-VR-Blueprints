using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour {
    public DrawLineManager manager;
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device dev;
    private SteamVR_TrackedController controller;
    //private Stack<GameObject> stack;


    // Use this for initialization
    void Start () {
        //SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadClicked += Controller_PadClicked;
   

    }

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
        //SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        if (dev.GetAxis().x <= 0)
        {
            //dev = SteamVR_Controller.Input((int)trackedObj.index);
            UnityEditor.Undo.DestroyObjectImmediate(manager.go);
            //    Debug.Log("Should destroy");
            //    Destroy(manager.objList.Pop());
            
            // Destroy(manager.go);
        }
    }


  
  
    // Update is called once per frame
    void Update () {
      
        dev = SteamVR_Controller.Input((int)trackedObj.index);
        //stack = manager.objList;
        /*if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !manager.toggle)
        {
            //Debug.LogError("got here");
           




        }
      */
		
	}

}
