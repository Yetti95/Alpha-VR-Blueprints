using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SLManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveLoad.Save();
        SaveLoad.Load();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
