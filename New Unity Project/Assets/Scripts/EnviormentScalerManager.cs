using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentScalerManager : MonoBehaviour
{
    public GameObject CameraRig;
    public SteamVR_TrackedObject left;
    public SteamVR_TrackedObject right;
    private float previousDiff = 0f;
    
    //public GameObject obj;
    // Use this for initialization
    void Start()
    {
        //Thoughts here is as the controllers move away from each other the mesh will scale up and grow. opposite if moved together
        //thoughts here is as the controllers are move away the mesh will move match the motion of the controller

    }

    // Update is called once per frame
    void Update()
    {
        var leftDev = SteamVR_Controller.Input((int)left.index);
        var rightDev = SteamVR_Controller.Input((int)right.index);
        if (leftDev.GetPress(SteamVR_Controller.ButtonMask.Grip) && rightDev.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            //Debug.LogError("Grips should scale");
            float diff = (left.transform.position - right.transform.position).magnitude;
            float delta = diff - previousDiff;
            
            //this is to help counter the noise recieved from the vr
            if (Mathf.Abs(delta) > 0.01f)
            {
                CameraRig.transform.localScale += (Vector3.one * Time.deltaTime * Mathf.Sign(delta) * 3f);
                

            }
            previousDiff = diff;

        }

    }
}

   