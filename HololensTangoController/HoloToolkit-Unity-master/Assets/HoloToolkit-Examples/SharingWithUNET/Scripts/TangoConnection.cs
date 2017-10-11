using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class TangoConnection : NetworkBehaviour {


    
    public bool getTangoConnected()
    {

        return TangoConnected;
    }

    bool TangoConnected = false;
	// Use this for initialization
	void Start () {
        TangoConnected = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
