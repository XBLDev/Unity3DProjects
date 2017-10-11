using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NetworkedPhoneMovement : NetworkBehaviour {



    private float AccelerometerUpdateInterval  = 1.0f / 60.0f;
    private float LowPassKernelWidthInSeconds  = 1.0f;


    private float LowPassFilterFactor = 0f; // tweakable

    private Vector3 lowPassValue = Vector3.zero;


    public float getPhoneX()
    {
        return phoneX;
    }

    public float getPhoneY()
    {
        return phoneY;
    }

    public float getPhoneZ()
    {
        return phoneZ;
    }


    public float getZ()
    {
        return accelerometerZ;
        //return phoneZ;

    }

    public float getX()
    {
        return accelerometerZ;
        //return phoneX;

    }

    public float getY()
    {
        return accelerometerZ;
        //return phoneY;

    }

    public void resetTranslations()
    {

    }

    float accelerometerZ = 0f;//Controller move up and down
    float accelerometerX = 0f;//Controller move up and down
    float accelerometerY = 0f;//Controller move up and down

    float previousAccelerometerX = -99999f;
    float currentAccelerometerX = -99999f;
    float previousAccelerometerY = -99999f;
    float currentAccelerometerY = -99999f;
    float previousAccelerometerZ = -99999f;
    float currentAccelerometerZ = -99999f;




    float phoneY = 0f;
    float phoneX = 0f;
    float phoneZ = 0f;

    float lastRecordedPhoneX = 0f;
    float lastRecordedPhoneY = 0f;
    float lastRecordedPhoneZ = 0f;




    // Use this for initialization
    void Start () {
        LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;

        lowPassValue = Input.acceleration;




    }

    /*
     *       z    
     *       |   
     *    ######
     *    #    # 
     *    #    # 
     *    #    # - x
     *    #    # 
     *    #    #     
     *    ######
     *       |   
     *       z
     * */


    // Update is called once per frame
    void Update () {
        //accelerometerZ = Input.acceleration.z;
        //accelerometerZ = Camera.main.gameObject.transform.position.y;//screen 

        phoneY = Camera.main.gameObject.transform.position.y;//screen 
        phoneX = Camera.main.gameObject.transform.position.x;
        phoneZ = Camera.main.gameObject.transform.position.z;

        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);

        accelerometerX = lowPassValue.x;//screen 
        accelerometerY = lowPassValue.y;//screen 
        accelerometerZ = lowPassValue.z;//screen 



    }
}
