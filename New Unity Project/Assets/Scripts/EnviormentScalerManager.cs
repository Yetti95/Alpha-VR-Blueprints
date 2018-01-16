using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentScalerManager : MonoBehaviour {
    public EnviormentScaler[] hands;
    public GameObject mesh;
    public bool scalable;
    //public GameObject obj;
    // Use this for initialization
    void Start() {

        Vector3 MeshScale = getMeshScale;
        Vector3 MeshLoc = getMeshScale;
        //Thoughts here is as the controllers move away from each other the mesh will scale up and grow. opposite if moved together
        //thoughts here is as the controllers are move away the mesh will move match the motion of the controller

    }

    // Update is called once per frame
    void Update() {
        scalable = (hands[0].pressed && hands[1].pressed);
        if (scalable)
        {
            setMeshScale(getDist());
        }
        //Iterate over hands, check for inputs
        foreach (EnviormentScaler h in hands)
        {
            if (h.device == null) continue;

            if (h.device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                print(h.gameObject.name + "pressed down");

            } else if (h.device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                print(h.gameObject.name + "released");
            }
        }
    }

    //This gets the current scale of the mesh/terrian that we imported
    public Vector3 getMeshScale
    {
        get { return mesh.transform.localScale; }
    }

    //This gets the current position of the mesh/terrain that we imported
    public Vector3 getMeshPos
    {
        get { return mesh.transform.localPosition; }
    }

    //Theis sets the current scale of the mesh to a given value
    public void setMeshScale(Vector3 change)
    {
        mesh.transform.localScale = change;
    }

    //This sets the current position of the mesh to a given value
    public void setMeshPos(Vector3 change)
    {
        mesh.transform.localPosition = change;
    }

    //gets the distance from each controller
    public Vector3 getDist()
    {

        Vector3 left = hands[0].device.transform.pos;
        Vector3 right = hands[1].device.transform.pos;
        Vector3 diff = left - right;
        return diff;
    }
}
