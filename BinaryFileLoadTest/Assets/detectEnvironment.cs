using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectEnvironment : MonoBehaviour {



    public GameObject TangoARCam;
    public GameObject normalCam;
    public GameObject TangoManagerObj;
	// Use this for initialization
	void Start () {
        if (Application.platform != RuntimePlatform.Android)
        {
            TangoManagerObj.gameObject.SetActive(false);
            TangoARCam.gameObject.SetActive(false);
            normalCam.gameObject.SetActive(true);
            normalCam.gameObject.tag = "MainCamera";
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
