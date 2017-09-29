using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour {


    public float zoomValue = 5f;

    public void zoomIn()
    {
        Camera.main.fieldOfView -= zoomValue;
    }

    public void zoomOut()
    {
        Camera.main.fieldOfView += zoomValue;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
