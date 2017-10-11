#if !UNITY_WSA_10_0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TangoRotationRecorder :  NetworkBehaviour{


    public GameObject TangoPhoneParentPrefab;


    GameObject TangoPhoneParent;
    Quaternion lastRecordedRot = new Quaternion();

    Quaternion lastSavedRot = new Quaternion();

    bool justReset = false;

    [Command]
    void CmdGetNewTangoPhoneParent()
    {
        if (TangoPhoneParentPrefab)
        {
            TangoPhoneParent = (GameObject)Instantiate
                (
                    TangoPhoneParentPrefab,
                    new Vector3(),
                    new Quaternion()
                );
        }
    }



        // Use this for initialization
    void Start () {

        CmdGetNewTangoPhoneParent();


        if (TangoPhoneParent !=null)
        {
            Quaternion currentPhoneRot = Camera.main.gameObject.transform.rotation;
            Vector3 currentPhonePos = Camera.main.gameObject.transform.position;

            TangoPhoneParent.gameObject.transform.position = currentPhonePos;
            TangoPhoneParent.gameObject.transform.rotation = currentPhoneRot;




            Camera.main.gameObject.transform.SetParent(TangoPhoneParent.gameObject.transform);

            lastRecordedRot = Camera.main.gameObject.transform.localRotation;
            lastSavedRot = Camera.main.gameObject.transform.localRotation;


        }

	}

    //[Command]
    //void CmdResetTangoControllerParentInScene()
    //{
    //    GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];
    //    GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];

    //    tangoControllerParentInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
    //    tangoControllerParentInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;

    //    tangoControllerInScene.gameObject.transform.position = Camera.main.gameObject.transform.position;
    //    tangoControllerInScene.gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;
    //}

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
    }

    public void setNewParentTransform()
    {

        saveCurrentRot();
        Quaternion currentPhoneRot = Camera.main.gameObject.transform.rotation;
        Vector3 currentPhonePos = Camera.main.gameObject.transform.position;

        Camera.main.gameObject.transform.SetParent(null);


        TangoPhoneParent.gameObject.transform.position = currentPhonePos;
        TangoPhoneParent.gameObject.transform.rotation = currentPhoneRot;

        //CmdResetTangoControllerParentInScene();



        Camera.main.gameObject.transform.SetParent(TangoPhoneParent.gameObject.transform);
        justReset = true;
    }


    public void saveCurrentRot()
    {
        lastSavedRot = lastRecordedRot;
    }

    public Quaternion getLastSavedRot()
    {
        return lastSavedRot;
    }

    public Quaternion getLastRecordedRot()
    {
        return lastRecordedRot;
    }

    public bool getJustReset()
    {
        return justReset;
    }

    public Quaternion getActualRot()
    {
        //return Quaternion.Inverse(lastSavedRotQua) * lastRecordedRotQua;
        return Quaternion.Inverse(lastSavedRot) * lastRecordedRot;
    }

    // Update is called once per frame
    void Update () {

        lastRecordedRot = Camera.main.gameObject.transform.rotation;
        if(justReset)
        {
            justReset = false;
        }




    }
}
#endif