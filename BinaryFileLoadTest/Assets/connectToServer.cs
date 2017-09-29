using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class connectToServer : MonoBehaviour {


    NetworkClient myClient;
    public InputField networkField;
    public Text networkText;
    //int adsfasdfdsafsdfa;
    bool connectedToServer = false;

    // Use this for initialization
    void Start()
    {

        WWW www = new WWW("152.83.70.50");
        while (!www.isDone)
        {
            //text.text = fileType + " : DOWNLOADING...";
            //www.
            //textBoard.text = fileType + " BINARY FILE LOADING, PROGRESS: " + www.progress;

        }


        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.Log("IP ADDRESS READ");
        }
        else
        {
            networkText.text = "IP ADDRESS READ";
        }

        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnDisConnected);
        //Network.useNat = true;
        //myClient.ReconnectToNewHost("152.83.70.50", 4444);
        myClient.Connect("152.83.70.50", 57183);//2405:b000:606:64::70:24 OR 152.83.70.50
    }

    // Update is called once per frame
    void Update()
    {


        //if (!myClient.isConnected)
        //{
        //    networkText.text = "connecting: 152.83.70.50";
        //    myClient.Connect("152.83.70.50", 57183);
        //    myClient.ReconnectToNewHost("152.83.70.50", 57183);
        //}
        //else
        //{
        //    networkText.text = "connectedTo: 152.83.70.50";

        //}
    }

    //void ConnectToServer()
    //{



    //    if (myClient != null)
    //    {
    //        myClient.ReconnectToNewHost("152.83.70.50", 4444);

    //    }
    //    //Network.Connect("152.83.70.50", 4444);
    //    networkText.text = "connecting...";
    //}

    

    //private void OnConnectedToServer()
    //{
    //    connectedToServer = true;
    //    networkText.text = "connected";
    //}

    //void OnFailedToConnect(NetworkConnectionError error)
    //{
    //    //Debug.Log("Could not connect to server: " + error);
    //    networkText.text = "notconnected";

    //}

    // client function : connected
    public void OnConnected(NetworkMessage netMsg)
    {
        networkText.text = "(MSG)connectedTo: 152.83.70.50";
    }

    //client function: not connected
    public void OnDisConnected(NetworkMessage netMsg)
    {
        networkText.text = "(MSG)notconnected";

    }
}
