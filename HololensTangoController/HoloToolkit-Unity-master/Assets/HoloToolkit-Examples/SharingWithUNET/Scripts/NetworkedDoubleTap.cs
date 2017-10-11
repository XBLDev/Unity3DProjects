
#if !UNITY_WSA_10_0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NetworkedDoubleTap : NetworkBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}


    float touchDuration;
    Touch touch;
    bool isDoubleTapping = false;
    public InputField doubleTapField;


    public bool getIsDoubleTapping()
    {
        return isDoubleTapping;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        { //if there is any touch
            touchDuration += Time.deltaTime;
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
                StartCoroutine("singleOrDouble");
        }
        else
        {
            isDoubleTapping = false;
            touchDuration = 0.0f;
            if (doubleTapField)
            {
                //doubleTapField.text = "no tap";
            }
        }
    }

    IEnumerator singleOrDouble()
    {
        yield return new WaitForSeconds(0.3f);
        if (touch.tapCount == 1)
        {
            isDoubleTapping = false;
            Debug.Log("Single");
            if (doubleTapField)
            {
                doubleTapField.text = "single tap";

            }
        }
        else if (touch.tapCount == 2)
        {
            isDoubleTapping = true;

            GetComponent<TangoRotationRecorder>().setNewParentTransform();



            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
            StopCoroutine("singleOrDouble");
            if (doubleTapField)
            {
                doubleTapField.text = "double tap";

            }
            Debug.Log("Double");
        }
    }




}
#endif