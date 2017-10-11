using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networkedDetectDeviceRotation : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;

    }


    public float getRotX()
    {
        return rotX;
    }


    public float getRotY()
    {
        return rotY;
    }


    public float getRotZ()
    {
        return rotZ;
    }

    public float rotX = 0f;
    public float rotY = 0f;
    public float rotZ = 0f;
    
    // Update is called once per frame
    void Update () {

        //player.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);

        //Input.gyro.r

        rotX = Input.gyro.attitude.x;
        rotY = Input.gyro.attitude.y;
        rotZ = Input.gyro.attitude.z;
        //Quaternion
    }
}
