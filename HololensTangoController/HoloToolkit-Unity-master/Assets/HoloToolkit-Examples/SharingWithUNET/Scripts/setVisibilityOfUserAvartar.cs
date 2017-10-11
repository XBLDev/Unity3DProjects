using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class setVisibilityOfUserAvartar : NetworkBehaviour {

    public Canvas UserCanvas;
    public GameObject UserMobileController;
    public GameObject UserCursorSphere;

	// Use this for initialization
	void Start () {


#if !UNITY_WSA_10_0
    if(UserCanvas)
    {
       UserCanvas.gameObject.SetActive(false);
    }
    if(UserMobileController)
    {
       UserMobileController.gameObject.SetActive(false);
    }
    if (UserCursorSphere)
    {
       UserCursorSphere.gameObject.SetActive(false);
    }


#endif



    }

    // Update is called once per frame
    void Update () {
		
	}
}
