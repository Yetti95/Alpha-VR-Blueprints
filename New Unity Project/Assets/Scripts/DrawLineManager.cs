using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private GraphicsLineRender currLine;
    private int numClicks = 0;
    public bool toggle;
    // Update is called once per frame
    private void Start()
    {
         toggle = true;
         currLine.setWidth(.1f);
    }
    void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && toggle)
        {

            /**
             * For the undo button:
             * 
             * possible array of objects so we can destroy on length of array - 1 
             * to undo the last object rendered
             * 
             * */
            //Debug.LogError("got here");
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter> ();
            go.AddComponent<MeshRenderer>();
            currLine = go.AddComponent<GraphicsLineRender>();

            //go.AddComponent<LineRenderer>();
            //currLine = go.AddComponent<LineRenderer>();

           

            numClicks = 0;

        } else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && toggle)
            {
            // currLine.SetVertexCount(numClicks + 1);
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
                
            }
        /*
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (toggle)
            {
                toggle = false;

            }
            else
            {
                toggle = true;
            }
        }
        */

    }

    public void setToggle(bool offOn)
    {
        toggle = offOn;
    }
    public bool getToggle()
    {
        return toggle;
    }

    public float getWidth()
    {
        return currLine.getWidth();
    }
    public void setWidth(float change)
    {
        currLine.setWidth(change);
    }
}
