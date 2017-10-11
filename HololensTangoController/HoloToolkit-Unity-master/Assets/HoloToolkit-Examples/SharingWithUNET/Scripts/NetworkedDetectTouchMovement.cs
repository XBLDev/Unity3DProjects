
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkedDetectTouchMovement : NetworkBehaviour
{

    public InputField pinchDistanceField;

    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 0;

    const float pinchRatio = 1;
    const float minPinchDistance = 0;

    const float panRatio = 1;
    const float minPanDistance = 0;

    static public float turnAngleDelta;
    static public float turnAngle;

    static public float pinchDistanceDelta;
    static public float pinchDistance;

    public bool isPinching = false;
    public bool isSingleTouching = false;


    void Update()
    {
        Calculate();
    }

    public bool ifPinching()
    {
        return isPinching;
    }

    public bool ifSingleTouching()
    {
        return isSingleTouching;
    }

    public float getPinchDistance()
    {
        //return pinchDistanceDelta;
        return pinchDistance;
    }
    

    public void Calculate()
    {
        pinchDistance = pinchDistanceDelta = 0;
        turnAngle = turnAngleDelta = 0;

        // if two fingers are touching the screen at the same time ...
        if (Input.touchCount == 2)
        {
            isSingleTouching = false;
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            // ... if at least one of them moved ...
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // ... check the delta distance between them ...
                pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                      touch2.position - touch2.deltaPosition);
                pinchDistanceDelta = pinchDistance - prevDistance;

                // ... if it's greater than a minimum threshold, it's a pinch!
                if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
                {
                    pinchDistanceDelta *= pinchRatio;
                    isPinching = true;
                    if (pinchDistanceField)
                    {
                        pinchDistanceField.text = "Pinching: " + isPinching + ", pinching distance: " + pinchDistance;
                    }
                }
                else
                {
                    isPinching = false;
                    pinchDistance = pinchDistanceDelta = 0;
                    if (pinchDistanceField)
                    {
                        pinchDistanceField.text = "Pinching: " + isPinching;
                    }
                }

                // ... or check the delta angle between them ...
                //turnAngle = Angle(touch1.position, touch2.position);
                //float prevTurn = Angle(touch1.position - touch1.deltaPosition,
                //                       touch2.position - touch2.deltaPosition);
                //turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                //// ... if it's greater than a minimum threshold, it's a turn!
                //if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                //{
                //    turnAngleDelta *= pinchTurnRatio;
                //}
                //else
                //{
                //    turnAngle = turnAngleDelta = 0;
                //}
            }
            else
            {
                isPinching = false;
                isSingleTouching = true;
                pinchDistance = pinchDistanceDelta = 0;

                if (pinchDistanceField)
                {
                    pinchDistanceField.text = "Pinching: " + isPinching;
                }
            }
        }
        else
        {
            isPinching = false;
            pinchDistance = pinchDistanceDelta = 0;

            if (pinchDistanceField)
            {
                pinchDistanceField.text = "Pinching: " + isPinching;
            }
        }

    }

    public float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);

        float result = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }

        return result;
    }
}
