using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCanvasCamera : MonoBehaviour {


    public GameObject PCCam;
    public GameObject TangoCam;
	// Use this for initialization
	void Start () {
		if(Application.platform == RuntimePlatform.Android)
        {
            this.GetComponent<Canvas>().worldCamera = TangoCam.GetComponent<Camera>();
        }
        else
        {
            this.GetComponent<Canvas>().worldCamera = PCCam.GetComponent<Camera>();

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
