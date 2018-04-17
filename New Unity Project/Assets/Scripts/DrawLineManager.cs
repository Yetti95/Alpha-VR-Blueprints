using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_TrackedController controller;
    private GraphicsLineRender currLine;
    public ColorManager cm;
    private int numClicks = 0;
    public bool toggle;
    //public Stack<GameObject> objList;
    public Material lmat;
    public GameObject go;
    public Draggable dragginSatuation;
    public Draggable dragginColor;

    // Update is called once per frame
    private void Start()
    {
        setWidth(.1f);
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadClicked += Controller_PadClicked;
       //objList = new Stack<GameObject>();
        
    }

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
       
    }

    void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
       
       toggle = dragginColor.dragging || dragginSatuation.dragging;

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !toggle)       {

        
            go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            //color is added to the mat as a copy of the mat plus color
            currLine = go.AddComponent<GraphicsLineRender>();
            currLine.lmat = new Material(lmat);
        
           

            numClicks = 0;

        } else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && !toggle)
            {
         
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
                
            }
        if(currLine != null)
        {
            currLine.lmat.color = ColorManager.Instance.GetCurrentColor();
           // objList.Push(go);
        }
        /*
        if (!objList.Contains(go))
        {
            objList.Push(go);
            Debug.Log("instance count: " + objList.Count);
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
