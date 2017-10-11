using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPhoneRotation : NetworkBehaviour {




    float TangoPhoneRotX = 0f;
    float TangoPhoneRotY = 0f;
    float TangoPhoneRotZ = 0f;

    float TangoPhoneRotEulerAngleX = 0f;
    float TangoPhoneRotEulerAngleY = 0f;
    float TangoPhoneRotEulerAngleZ = 0f;


    Quaternion TangoPhoneRotation;


    public float getTangoPhoneRotX()
    {
        return TangoPhoneRotX;
    }

    public float getTangoPhoneRotY()
    {
        return TangoPhoneRotY;
    }

    public float getTangoPhoneRotZ()
    {
        return TangoPhoneRotZ;
    }

    public float getTangoPhoneRotEulerAngleX()
    {
        return TangoPhoneRotEulerAngleX;
    }

    public float getTangoPhoneRotEulerAngleY()
    {
        return TangoPhoneRotEulerAngleY;
    }

    public float getTangoPhoneRotEulerAngleZ()
    {
        return TangoPhoneRotEulerAngleZ;
    }

    public Quaternion getTangoPhoneRotation()
    {
        return TangoPhoneRotation;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        TangoPhoneRotX = Camera.main.gameObject.transform.rotation.x;
        TangoPhoneRotY = Camera.main.gameObject.transform.rotation.y;
        TangoPhoneRotZ = Camera.main.gameObject.transform.rotation.z;

        //target.transform.eulerAngles.x
        TangoPhoneRotEulerAngleX = Camera.main.gameObject.transform.eulerAngles.x;
        TangoPhoneRotEulerAngleY = Camera.main.gameObject.transform.eulerAngles.y;
        TangoPhoneRotEulerAngleZ = Camera.main.gameObject.transform.eulerAngles.z;

        //var target = GameObject.Find("center");
        //Vector3 newRotation = new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, target.transform.eulerAngles.z);
        //this.transform.eulerAngles = newRotation;


        TangoPhoneRotation = Camera.main.gameObject.transform.rotation;



    }
}
