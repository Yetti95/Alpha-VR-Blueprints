using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushScaler : MonoBehaviour {


    public DrawLineManager manager;
    public SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device dev;
    private SteamVR_TrackedController controller;
    private float currentWidth;
    private float prevPos = 0f;
    private float prevDif = 0f;
    private int rate = 0;
    //private SteamVR_Controller.Device device;

    // Use this for initialization
    void Start () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadClicked += Controller_PadClicked;
        //device = SteamVR_Controller.Input((int)trackedObj.index);
        currentWidth = manager.getWidth();
        //currPos = device.GetTouch(SteamVR_Controller.Input.);
        //prevPos = 0f;
       // prevPos = device.GetAxis().x;
        
    }
    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
       

    }

    /*
     *    this almost works. The size change is off.
     *    The best way to describe it is the edges of the pad set it large
     *    While the middle sets the scale small.
     *    We want left edge to reduce size and right edge to increase size
     */
    // Update is called once per frame
    void FixedUpdate () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        print("previous position: " + prevPos);


        if (rate >= 5)
        {
            /*
            float diff = (device.GetAxis().x - prevPos);
            float delta = diff - prevDif;
            if (Mathf.Abs(delta) > .05f)
            {
                prevDif = (device.GetAxis().x - prevPos);
                prevPos = device.GetAxis().x;
                manager.setWidth(delta);
                rate = 0;
                //prevPos = device.GetAxis().x;
            }
            */
            if(device.GetAxis().x < 0)
            {
                float diff = (device.GetAxis().x - prevPos);
                float delta = diff - prevDif;
                if (Mathf.Abs(delta) > .05f)
                {
                    prevDif = (device.GetAxis().x - prevPos);
                    prevPos = device.GetAxis().x;
                    manager.setWidth(delta);
                    rate = 0;
                    manager.setWidth(manager.getWidth() * Time.deltaTime * Mathf.Sign(delta) * 3f);
                    //prevPos = device.GetAxis().x;
                }
            }
            else
            {
                float diff = (device.GetAxis().x - prevPos);
                float delta = diff - prevDif;
                if (Mathf.Abs(delta) > .05f)
                {
                    prevDif = (device.GetAxis().x - prevPos);
                    prevPos = device.GetAxis().x;
                    manager.setWidth(delta);
                    rate = 0;
                    manager.setWidth(manager.getWidth() / (Time.deltaTime * Mathf.Sign(delta) * 2f));
                    //prevPos = device.GetAxis().x;
                }
            }

        }
        rate++;
    /*
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.LogError("got here");
           // GameObject go = new GameObject();
           // go.AddComponent<MeshFilter>();
           // go.AddComponent<MeshRenderer>();
           // currLine = go.AddComponent<GraphicsLineRender>();

            //go.AddComponent<LineRenderer>();
            //currLine = go.AddComponent<LineRenderer>();

            

            

        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //currLine.SetVertexCount(numClicks + 1);
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            //currLine.AddPoint(trackedObj.transform.position);
            //numClicks++;

        }
        */
        
    }
}
