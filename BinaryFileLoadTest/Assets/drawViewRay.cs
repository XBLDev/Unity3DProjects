using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawViewRay : MonoBehaviour {


    LineRenderer lr;
    public Transform rayEndPoint;
    public Transform rayEndPointPC;

    //Transform rayEnd;
	// Use this for initialization
	void Start () {
        lr = this.gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        if (Application.platform != RuntimePlatform.Android)
        {
            lr.startWidth = 0.5f;
            lr.endWidth = 0.5f;

        }
        else
        {
            lr.startWidth = 0.5f * 0.025f;
            lr.endWidth = 0.5f * 0.025f;
        }
        //lr.useWorldSpace = false;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(lr)
        {
            //if (Application.platform == RuntimePlatform.Android)
            //{
                Vector3 camPos = Camera.main.gameObject.transform.position;
            //}
            //Vector3 endPos = camPos + Vector3.forward * 10;
            //transform.position += Vector3.forward * Time.deltaTime;
            //lr.numPositions = 2;
            Vector3 raystartPos = new Vector3(camPos.x, camPos.y + 0.5f, camPos.z);
            Vector3 rayendpos = new Vector3();
            if (Application.platform != RuntimePlatform.Android)
            {
                rayendpos = rayEndPointPC.position;
            }
            else
            {
                rayendpos = rayEndPoint.position;
            }

            lr.startColor = new Color(0, 255, 0, 255);
            lr.endColor = new Color(0, 255, 0, 0);
            lr.SetPosition(0, raystartPos);
            lr.SetPosition(1, rayendpos);

            
        }

    }
}
