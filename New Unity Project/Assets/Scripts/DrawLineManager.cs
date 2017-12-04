﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private LineRenderer currLine;
    private int numClicks = 0;
	// Update is called once per frame
	void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.LogError("got here");
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter> ();
            go.AddComponent<MeshRenderer>();
            currLine = go.AddComponent<GraphicsLineRender>();

            //go.AddComponent<LineRenderer>();
            //currLine = go.AddComponent<LineRenderer>();

            currLine.SetWidth(.1f, .1f);

            numClicks = 0;

        } else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                currLine.SetVertexCount(numClicks + 1);
                currLine.SetPosition(numClicks, trackedObj.transform.position);
                numClicks++;
                
            }

	}
}
