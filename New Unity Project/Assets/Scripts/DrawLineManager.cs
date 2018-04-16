using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_TrackedController controller;
    //public SteamVR_TrackedObject left;
    private GraphicsLineRender currLine;
    public ColorManager cm;
    private int numClicks = 0;
    public bool toggle;
    public Stack<GameObject> objList;
    public Material lmat;
    public Draggable dragginSatuation;
    public Draggable dragginColor;

    // Update is called once per frame
    private void Start()
    {
        setWidth(.1f);
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadClicked += Controller_PadClicked;
        
    }

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
       
    }

    void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        //SteamVR_Controller.Device leftDev = SteamVR_Controller.Input((int)left.index);
       toggle = dragginColor.dragging || dragginSatuation.dragging;

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !toggle)       {

            /**
             * For the undo button:
             * 
             * possible array of objects so we can destroy on length of array - 1 
             * to undo the last object rendered
             * 
             * */
            //Debug.LogError("got here");
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            //color not getting added to the game object
            //go.GetComponent<MeshRenderer>().material.color = cm.color;
            currLine = go.AddComponent<GraphicsLineRender>();
            currLine.lmat = new Material(lmat);

            //currLine.lmat.color = cm.color;
            //objList.Push(go);
            //go.AddComponent<LineRenderer>();
            //currLine = go.AddComponent<LineRenderer>();
            //print(objList.Count);
           

            numClicks = 0;

        } else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && !toggle)
            {
            // currLine.SetVertexCount(numClicks + 1);
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
                
            }
        if(currLine != null)
        {
            currLine.lmat.color = ColorManager.Instance.GetCurrentColor();
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
        Debug.Log("instance count: " + objList.Count);
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
