using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkedRayCast : NetworkBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    
    public GameObject getRayCastObj()
    {
        return rayCastObj;
    }

    public GameObject cameraCursorSphere;
    GameObject rayCastObj;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            rayCastObj = objectHit.gameObject;
            if(cameraCursorSphere)
            {
                cameraCursorSphere.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;

            }
        }
        else
        {
            rayCastObj = null;
            if (cameraCursorSphere)
            {
                cameraCursorSphere.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;

            }
        }
    }









}
