using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;




public class NetworkedDetectLongTouch : NetworkBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    private float holdTime = 0.4f; //or whatever
    private float acumTime = 0;

    public InputField HoldGestureField;
    bool isHolding = false;

    public bool getIsHolding()
    {
        return isHolding;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            acumTime += Input.GetTouch(0).deltaTime;

            if (acumTime >= holdTime)
            {

                isHolding = true;

                if (HoldGestureField)
                {
                    HoldGestureField.text = "Long Hold";
                }
                //Long tap
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                isHolding = false;
                acumTime = 0;

                if (HoldGestureField)
                {
                    HoldGestureField.text = "No Long Hold";
                }
            }
        }
        else
        {
            isHolding = false;
            acumTime = 0;
            if (HoldGestureField)
            {
                HoldGestureField.text = "No Long Hold";
            }
        }
    }




}
