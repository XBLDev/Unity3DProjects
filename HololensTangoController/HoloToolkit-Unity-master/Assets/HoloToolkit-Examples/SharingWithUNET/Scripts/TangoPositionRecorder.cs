#if !UNITY_WSA_10_0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class TangoPositionRecorder : NetworkBehaviour {



    [Command]
    void CmdResetTangoControllerParentInScene()
    {
        RpcResetTangoControllerParentInScene();
    }

    [ClientRpc]
    public void RpcResetTangoControllerParentInScene()
    {
        ResetTangoControllerParentInScene();

    }

    public void ResetTangoControllerParentInScene()
    {
        GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];
        GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];

        tangoControllerParentInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
        tangoControllerParentInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;

        tangoControllerInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
        tangoControllerInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;
        lastRecordedPos = tangoControllerParentInScene.gameObject.transform.position;

    }


    Vector3 lastRecordedPos = new Vector3();

	// Use this for initialization
	void Start () {
        //GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];
        //GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];

        //tangoControllerParentInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
        //tangoControllerParentInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;

        //tangoControllerInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
        //tangoControllerInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;
        //CmdResetTangoControllerParentInScene();

    }



    public Vector3 getLastRecordedPos()
    {
        return lastRecordedPos;
    }




    // Update is called once per frame
    void Update () {

        //lastRecordedPos = Camera.main.gameObject.transform.localPosition;
        //GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];
        //CmdResetTangoControllerParentInScene();
        //GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];

        //tangoControllerInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
        //tangoControllerInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;
        //tangoControllerInScene.gameObject.transform.SetParent(tangoControllerParentInScene.gameObject.transform, true);

        //lastRecordedPos = tangoControllerParentInScene.gameObject.transform.position;
        lastRecordedPos = Camera.main.gameObject.transform.position;

        //lastRecordedPos = tangoControllerInScene.gameObject.transform.localPosition;
        //Vector3 currentTangoControllerInScenePos = tangoControllerInScene.gameObject.transform.position;
        //Quaternion currentTangoControllerInSceneRot = tangoControllerInScene.gameObject.transform.rotation;
        //tangoControllerInScene.gameObject.transform.SetParent(null, true);



    }
}
#endif