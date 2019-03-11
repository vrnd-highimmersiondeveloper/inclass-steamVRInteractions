using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MyInteractions : MonoBehaviour {

    [SteamVR_DefaultAction("Teleport", "default")]
    public SteamVR_Action_Boolean button; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
