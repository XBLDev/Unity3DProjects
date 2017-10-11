using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;

namespace HoloToolkit.Examples.SharingWithUNET
{
    public class chat : NetworkBehaviour
    {


        //public List<string> chatHistory = new List<string>();


        public Button sendButton;

        //public List<InputField> chatWindows;
        public InputField inputField;


        public InputField myChatBox;
        public InputField msgBoard;
        public InputField nameField;
        public InputField scrollChatField;
        public Scrollbar scrollbar;

        public Text chatText;
        public Image chatImage;
        // Use this for initialization
        //public InputField publicMsgBoard;


        [SyncVar(hook = "OnChangeCurrentMsg")]
        //[SyncVar]
        public string currentMsg = "";



        GameObject[] chatRooms;
        //chatRoom TheChatRoom;
        bool sent = false;
        /*
        [Command]
        public void CmdOnChangeCurrentMsg(string current)
        {

            //chatRoom cr = chatRooms[0].GetComponent<chatRoom>();
            //myChatBox.text = cr.getLastHistory();
            //GameObject[] playersONLINE = GameObject.FindGameObjectsWithTag("Player");

            //for (int i = 0; i < playersONLINE.Length; i++)
            //{
            //    chat chatScript = playersONLINE[i].GetComponent<chat>();
            //    chatScript.updateChatBox(current + " " + playersONLINE.Length + " players");


            //}


            myChatBox.text = current;
        }

        */
        //NetworkClient myClient;
        //NetworkClient myClient1;





        public class MyMsgType
        {
            public static short Score = MsgType.Highest + 1;
            //public static short Voice = MsgType.Fragment;

        };


        public class MyMsgType2
        {

            public static short Voice = MsgType.Highest + 2;

        };

        public class RequestMsgType
        {

            public static short Request = MsgType.Highest + 3;

        };

        public class NextBugRequestMsgType
        {
            public static short NextBugRequest = MsgType.Highest + 4;

        }

        public class AgreeNextBugMsgType
        {
            public static short AgreeNextBug = MsgType.Highest + 5;
        }

        public class ShowNextBugMsgType
        {
            public static short ShowNextBug = MsgType.Highest + 6;
        }

        public class MyPointerAtBugPositionMsgType
        {
            public static short MyPointerPosition = MsgType.Highest + 7;

        }

        public class BugPaintingMsgType
        {
            public static short BugPainting = MsgType.Highest + 8;

        }

        public class TangoPinchDistanceMsgType
        {
            public static short TangoPinchDistance = MsgType.Highest + 9;
        }

        public class TangoGyroDataMsgType
        {
            public static short TangoGyroData = MsgType.Highest + 10;
        }

        public class TangoAccelerometerZMsgType
        {
            public static short TangoAccelerometerZData = MsgType.Highest + 11;
        }

        public class TangoDoubleTapMsgType
        {
            public static short TangoDoubleTapData = MsgType.Highest + 12;
        }


        public class TangoHoldingMsgType
        {
            public static short TangoHoldData = MsgType.Highest + 13;
        }

        public class TangoPhonePosMsgType
        {
            public static short TangoPhonePosData = MsgType.Highest + 14;
        }

        public class TangoPhoneRotMsgType
        {
            public static short TangoPhoneRotData = MsgType.Highest + 15;
        }

        public class TangoPhoneStartedMsgType
        {
            public static short TangoPhoneStartedData = MsgType.Highest + 16;
        }

        public class TangoPhoneRotQueMsgType
        {
            public static short TangoPhoneRotQueData = MsgType.Highest + 17;
        }

        public class TangoPhoneActualRotMsgType
        {
            public static short TangoPhoneActualRotData = MsgType.Highest + 18;
        }

        public class TangoPhoneLocalPosMsgType
        {
            public static short TangoPhoneLocalPosData = MsgType.Highest + 19;
        }

        public class TangoPhoneLastSavedRotMsgType
        {
            public static short TangoPhoneLastSavedRotData = MsgType.Highest + 20;
        }

        public class TangoPhoneJustResetMsgType
        {
            public static short TangoPhoneJustResetData = MsgType.Highest + 21;
        }

        public class TangoPhoneJustRecordedMsgType
        {
            public static short TangoPhoneJustRecordedData = MsgType.Highest + 22;
        }

        public void setGotBugPaintingMessageToSend(Vector3 input, bool drawing, bool erasing)
        {
            gotBugPaintingMessageToSend = true;
            paintPointPassedIn = input;
            paintPointIsDrawing = drawing;
            paintPointIsErasing = erasing;
        }


        public class TangoPhoneLastRecordedRotMessage : MessageBase
        {
            //public bool phoneStarted;
            public Quaternion lastRecordedRot;
        }

        public class TangoPhoneJustResetMessage : MessageBase
        {
            //public bool phoneStarted;
            public bool justReset;
        }

        public class TangoPhoneLastSavedRotMessage : MessageBase
        {
            //public bool phoneStarted;
            public Quaternion lastSavedRot;
        }

        public class TangoPhoneLocalPosMessage : MessageBase
        {
            //public bool phoneStarted;
            public Vector3 localPos;
        }

        public class TangoPhoneActualRotMessage : MessageBase
        {
            //public bool phoneStarted;
            public Quaternion actualRot;
        }


        public class TangoPhoneRotQueMessage : MessageBase
        {
            //public bool phoneStarted;
            public Quaternion rotation;
        }

        public class TangoPhoneStartedMessage : MessageBase
        {
            public bool phoneStarted;
        }


        public class TangoPhoneRotMessage : MessageBase
        {
            public float phoneRotY;
            public float phoneRotX;
            public float phoneRotZ;
        }

        public class TangoPhonePosMessage : MessageBase
        {
            public float phoneY;
            public float phoneX;
            public float phoneZ;
        }


        public class TangoHoldingMessage : MessageBase
        {
            public bool holding;
        }

        public class TangoDoubleTappingMessage : MessageBase
        {
            public bool doubleTapping;
        }


        public class TangoAcceleroMeterZMessage : MessageBase
        {
            public float acceleroMeterZ;
            public float phoneY;
            public float phoneX;
            public float phoneZ;
        }

        public class TangoGyroMessage : MessageBase
        {
            public float Gyrox;
            public float Gyroy;
            public float Gyroz;
        }

        public class TangoPinchDistanceMessage : MessageBase
        {
            public float pDistance;
        }


        bool paintPointIsDrawing = false;
        bool paintPointIsErasing = false;
        Vector3 paintPointPassedIn = new Vector3();
        bool gotBugPaintingMessageToSend = false;
        public class BugPaintingMessage : MessageBase
        {
            public string requestMsgFrom;

            public bool painting;
            public bool erasing;
            public float paintPosX;
            public float paintPosY;
            public float paintPosZ;
        }


        public class MyPointerAtBugPositionMessage : MessageBase
        {
            public string requestMsgFrom;

            public float myPointerPosX;
            public float myPointerPosY;
            public float myPointerPosZ;
        }

        public class ShowNextBugMessage : MessageBase
        {
            public string requestMsgFrom;
            public string requestMsgRecieved;
        }


        public class RequestMessage : MessageBase
        {
            public string requestMsgFrom;
            public string requestMsgRecieved;
        }


        public class NextBugRequestMessage : MessageBase
        {
            public string requestMsgFrom;
            public string requestMsgRecieved;
        }

        public class AgreeNextBugMessage : MessageBase
        {
            public string requestMsgFrom;
            public bool agreeOrNot;
        }

        public class ChatMessage : MessageBase
        {
            //public int score;
            //public Vector3 scorePos;
            //public int lives;
            public string msgRecieved;
            public string msgFrom;
            public string msgFromName;
        }


       public void sendTangoLastRecordedRot(Quaternion lastRecordedRot)
        {
            TangoPhoneLastRecordedRotMessage msg = new TangoPhoneLastRecordedRotMessage();

            msg.lastRecordedRot = lastRecordedRot;

            NetworkServer.SendToAll(TangoPhoneJustRecordedMsgType.TangoPhoneJustRecordedData, msg);

        }


        public void sendTangoLastSavedRot(Quaternion lastSavedRot)
        {
            TangoPhoneLastSavedRotMessage msg = new TangoPhoneLastSavedRotMessage();

            msg.lastSavedRot = lastSavedRot;

            NetworkServer.SendToAll(TangoPhoneLastSavedRotMsgType.TangoPhoneLastSavedRotData, msg);
        }

        public void sendTangoJustReset(bool justReset)
        {
            TangoPhoneJustResetMessage msg = new TangoPhoneJustResetMessage();

            msg.justReset = justReset;

            NetworkServer.SendToAll(TangoPhoneJustResetMsgType.TangoPhoneJustResetData, msg);
        }

        public void sendTangoPhoneLocalPos(Vector3 localPos)
        {
            TangoPhoneLocalPosMessage msg = new TangoPhoneLocalPosMessage();

            msg.localPos = localPos;

            NetworkServer.SendToAll(TangoPhoneLocalPosMsgType.TangoPhoneLocalPosData, msg);


        }

        public void sendTangoPhoneActualRot(Quaternion actualRot)
        {
            TangoPhoneActualRotMessage msg = new TangoPhoneActualRotMessage();

            msg.actualRot = actualRot;

            NetworkServer.SendToAll(TangoPhoneActualRotMsgType.TangoPhoneActualRotData, msg);

        }


        public void sendTangoPhoneRotQue(Quaternion rotque)
        {
            TangoPhoneRotQueMessage msg = new TangoPhoneRotQueMessage();

            msg.rotation = rotque;

            NetworkServer.SendToAll(TangoPhoneRotQueMsgType.TangoPhoneRotQueData, msg);
        }

        public void sendTangoStarted(bool TangoStarted)
        {
            TangoPhoneStartedMessage msg = new TangoPhoneStartedMessage();

            msg.phoneStarted = TangoStarted;

            NetworkServer.SendToAll(TangoPhoneStartedMsgType.TangoPhoneStartedData, msg);



        }





        public void sendTangoPhoneRot(float x, float y, float z)
        {
            TangoPhoneRotMessage msg = new TangoPhoneRotMessage();

            msg.phoneRotX = x;
            msg.phoneRotY = y;
            msg.phoneRotZ = z;

            NetworkServer.SendToAll(TangoPhoneRotMsgType.TangoPhoneRotData, msg);

        }

        public void sendTangoPhonePos(float x, float y, float z)
        {
            TangoPhonePosMessage msg = new TangoPhonePosMessage();
            msg.phoneX = x;
            msg.phoneY = y;
            msg.phoneZ = z;
            NetworkServer.SendToAll(TangoPhonePosMsgType.TangoPhonePosData, msg);


        }

        public void sendTangoholding(bool holding)
        {
            TangoHoldingMessage msg = new TangoHoldingMessage();
            msg.holding = holding;
            NetworkServer.SendToAll(TangoHoldingMsgType.TangoHoldData, msg);


        }

        public void sendTangoDoubleTapping(bool doubleTapping)
        {
            TangoDoubleTappingMessage msg = new TangoDoubleTappingMessage();
            msg.doubleTapping = doubleTapping;
            NetworkServer.SendToAll(TangoDoubleTapMsgType.TangoDoubleTapData, msg);

        }

        public void sendTangoAccelerometerZ(float z)
        {
            TangoAcceleroMeterZMessage msg = new TangoAcceleroMeterZMessage();
            msg.acceleroMeterZ = z;
            NetworkServer.SendToAll(TangoAccelerometerZMsgType.TangoAccelerometerZData, msg);



        }

        public void sendTangoGyroRotation(float rotX, float rotY, float rotZ)
        {
            TangoGyroMessage msg = new TangoGyroMessage();
            msg.Gyrox = rotX;
            msg.Gyroy = rotY;
            msg.Gyroz = rotZ;
            NetworkServer.SendToAll(TangoGyroDataMsgType.TangoGyroData, msg);
        }

        public void sendTangoPinchDistance(float tangoPinchDistance)
        {
            TangoPinchDistanceMessage msg = new TangoPinchDistanceMessage();
            msg.pDistance = tangoPinchDistance;
            NetworkServer.SendToAll(TangoPinchDistanceMsgType.TangoPinchDistance, msg);

        }

        public void sendBugPainting(Vector3 paintPos, string name, bool erasing, bool painting)
        {
            BugPaintingMessage msg = new BugPaintingMessage();
            msg.paintPosX = paintPos.x;
            msg.paintPosY = paintPos.y;
            msg.paintPosZ = paintPos.z;
            msg.requestMsgFrom = name;
            msg.erasing = erasing;
            msg.painting = painting;
            NetworkServer.SendToAll(BugPaintingMsgType.BugPainting, msg);

        }

        public void sendMyPointerPosition(Vector3 mypointerpos, string name)
        {
            MyPointerAtBugPositionMessage msg = new MyPointerAtBugPositionMessage();
            msg.myPointerPosX = mypointerpos.x;
            msg.myPointerPosY = mypointerpos.y;
            msg.myPointerPosZ = mypointerpos.z;
            msg.requestMsgFrom = name;

            NetworkServer.SendToAll(MyPointerAtBugPositionMsgType.MyPointerPosition, msg);

        }

        public void sendShowNextBug(string shownextbugmsg, string name)
        {
            ShowNextBugMessage msg = new ShowNextBugMessage();
            msg.requestMsgRecieved = shownextbugmsg;
            msg.requestMsgFrom = name;

            NetworkServer.SendToAll(ShowNextBugMsgType.ShowNextBug, msg);

        }

        public void sendAgreeNextBug(bool agreeOrNot, string name)
        {
            AgreeNextBugMessage msg = new AgreeNextBugMessage();
            msg.agreeOrNot = agreeOrNot;
            msg.requestMsgFrom = name;

            NetworkServer.SendToAll(AgreeNextBugMsgType.AgreeNextBug, msg);

        }

        public void sendNextBugRequest(string requestMsgToSend, string name)
        {
            NextBugRequestMessage msg = new NextBugRequestMessage();
            msg.requestMsgFrom = name;
            //this.netId.ToString();
            msg.requestMsgRecieved = requestMsgToSend;
            //"x: " + transform.position.x.ToString() + ", y: " + transform.position.y.ToString() + ", z: " + transform.position.z.ToString() ;

            NetworkServer.SendToAll(NextBugRequestMsgType.NextBugRequest, msg);
        }

        public void sendRequest(string requestMsgToSend, string name)
        {
            RequestMessage msg = new RequestMessage();
            msg.requestMsgFrom = name;
            //this.netId.ToString();
            msg.requestMsgRecieved = requestMsgToSend;
            //"x: " + transform.position.x.ToString() + ", y: " + transform.position.y.ToString() + ", z: " + transform.position.z.ToString() ;

            NetworkServer.SendToAll(RequestMsgType.Request, msg);
        }

        public void SendScore(string MessageToSend, string name)
        {
            //scrollbar.
            ChatMessage msg = new ChatMessage();

            msg.msgRecieved = MessageToSend;
            msg.msgFrom = this.netId.ToString();
            msg.msgFromName = name;

            NetworkServer.SendToAll(MyMsgType.Score, msg);
            Debug.Log("BroadCast: Msg sent by client: " + this.netId);
        }

        // Create a client and connect to the server port
        public void SetupClient()
        {

            //NetworkIdentity.connectionToClient ctc = 
            //NetworkIdentity.connectionToClient.RegisterHandler(MsgType.Connect, OnConnected);
            //NetworkBehaviour.connectionToClient.RegisterHandler(MsgType.Connect, OnConnected);


            //if (nc!=null)
            //{
            //    myChatBox.text = "GOT CLIENT";
            //    nc.RegisterHandler(MsgType.Connect, OnConnected);
            //    nc.RegisterHandler(MyMsgType.Score, OnScore);

            //}
            //else
            //{
            //    myChatBox.text = "NO CLIENT";
            //}
            //NetworkClient.RegisterHandler(MsgType.Connect, OnConnected);

            //myClient1 = 
            //NetworkClient.allClients

            //NetworkBehaviour nb = new NetworkBehaviour();
            //this.connectionToClient.RegisterHandler(MsgType.Connect, OnConnected);
            //nb.connectionToClient.RegisterHandler(MsgType.Connect, OnConnected);



            this.connectionToServer.RegisterHandler(MsgType.Connect, OnConnected);
            //this.connectionToServer.RegisterHandler(MyMsgType2.Voice, handleMsg);
            this.connectionToServer.RegisterHandler(MyMsgType.Score, OnScore);

            this.connectionToServer.RegisterHandler(RequestMsgType.Request, OnRequest);
            this.connectionToServer.RegisterHandler(NextBugRequestMsgType.NextBugRequest, OnNextBugRequest);
            this.connectionToServer.RegisterHandler(AgreeNextBugMsgType.AgreeNextBug, OnAgreeNextBug);
            this.connectionToServer.RegisterHandler(ShowNextBugMsgType.ShowNextBug, OnShowNextBug);
            this.connectionToServer.RegisterHandler(MyPointerAtBugPositionMsgType.MyPointerPosition, OnMyPointerPositionRecieved);
            this.connectionToServer.RegisterHandler
                (BugPaintingMsgType.BugPainting, onBugPaintingMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoPinchDistanceMsgType.TangoPinchDistance, onTangoPinchDistanceMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoGyroDataMsgType.TangoGyroData, onTangoGyroMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoAccelerometerZMsgType.TangoAccelerometerZData, onTangoAccelerometerZMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoDoubleTapMsgType.TangoDoubleTapData, onTangoDoubleTapMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoHoldingMsgType.TangoHoldData, onTangoHoldingMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoPhonePosMsgType.TangoPhonePosData, onTangoPhonePosMsgRecieved);
            this.connectionToServer.RegisterHandler
                (TangoPhoneRotMsgType.TangoPhoneRotData, onTangoPhoneRotMsgRecieved);

            this.connectionToServer.RegisterHandler
                (TangoPhoneStartedMsgType.TangoPhoneStartedData, onTangoPhoneStartedMsgRecieved);

            this.connectionToServer.RegisterHandler
                 (TangoPhoneRotQueMsgType.TangoPhoneRotQueData, onTangoPhoneRotQueMsgRecieved);

            this.connectionToServer.RegisterHandler
                  (TangoPhoneActualRotMsgType.TangoPhoneActualRotData, onTangoPhoneActualRotMsgRecieved);

            this.connectionToServer.RegisterHandler
                  (TangoPhoneLocalPosMsgType.TangoPhoneLocalPosData, onTangoPhoneLocalPosMsgRecieved);

            this.connectionToServer.RegisterHandler
                  (TangoPhoneJustResetMsgType.TangoPhoneJustResetData, onTangoPhoneJustResetMsgRecieved);

            this.connectionToServer.RegisterHandler
                  (TangoPhoneLastSavedRotMsgType.TangoPhoneLastSavedRotData, onTangoPhoneLastSavedRotMsgRecieved);

            this.connectionToServer.RegisterHandler
                  (TangoPhoneJustRecordedMsgType.TangoPhoneJustRecordedData, onTangoPhoneJustRecordedRotMsgRecieved);


            //if (isServer)
            //{
            //    this.connectionToClient.RegisterHandler(MsgType.Connect, OnConnected);
            //    this.connectionToClient.RegisterHandler(MyMsgType.Score, OnScore);
            //}
            //myClient = new NetworkClient();
            //myClient.RegisterHandler(MsgType.Connect, OnConnected);
            //myClient.RegisterHandler(MyMsgType.Score, OnScore);
            //myClient.Connect("127.0.0.1", 4444);
        }

        //public override void OnStartLocalPlayer()
        //{
        //    //GetComponent<MeshRenderer>().material.color = Color.blue;
        //    SetupClient();
        //    Debug.Log("Client BroadCast Config Success For: "+this.netId);
        //}



        public class VOICEMessage : MessageBase
        {
            //public int score;
            //public Vector3 scorePos;
            //public int lives;
            //public string msgRecieved;
            //public string msgFrom;
            //public string msgFromName;
            public Byte[] piece;
            //public int channel;
        }


        public void handleMsg(NetworkMessage netMsg)
        {


            //ClientRecieved = true;
            Debug.Log("CLIENT: RECIVED SOMETHING FROM SERVER");

            //return new NetworkMessageDelegate(handle);
        }

        Quaternion differenceBetweenTangoAndResetSpot = new Quaternion();

        public void onTangoPhoneJustRecordedRotMsgRecieved(NetworkMessage TangoPhoneJustRecordedRotMsg)
        {
            TangoPhoneLastRecordedRotMessage msg = TangoPhoneJustRecordedRotMsg.ReadMessage<TangoPhoneLastRecordedRotMessage>();

            Quaternion justRecordedRot = msg.lastRecordedRot;

            //GameObject tangoPhoneRotationIndicator = GameObject.FindGameObjectsWithTag("TangoControllerGlobalRotationIndicator")[0];

            //tangoPhoneRotationIndicator.gameObject.transform.position = mobileControllerSpawnPos.gameObject.transform.position;
            //tangoPhoneRotationIndicator.gameObject.transform.rotation = justRecordedRot;
            //tangoPhoneRotationIndicator.gameObject.transform.Translate(0,0.1f,0);

            //Quaternion from = tangoPhoneRotationIndicator.gameObject.transform.rotation;
            //Quaternion to = mobileControllerSpawnPos.gameObject.transform.rotation;
            //differenceBetweenTangoAndResetSpot = Quaternion.Inverse(from) * to;

            //if(from == to)
            //{
            //    tangoPhoneRotationIndicator.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            //}
            //else
            //{
            //    tangoPhoneRotationIndicator.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //}

            //tangoPhoneRotationIndicator.gameObject.transform.rotation = tangoPhoneRotationIndicator.gameObject.transform.rotation * differenceBetweenTangoAndResetSpot;

        }

        public void onTangoPhoneJustResetMsgRecieved(NetworkMessage TangoPhoneJustResetMsg)
        {
            TangoPhoneJustResetMessage msg = TangoPhoneJustResetMsg.ReadMessage<TangoPhoneJustResetMessage>();

            bool justReset = msg.justReset;

            if(justReset)
            {
                //GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];
                //GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];
                //tangoControllerParentInScene.gameObject.transform.position = tangoControllerInScene.gameObject.transform.position;

            }
        }

        public void onTangoPhoneLastSavedRotMsgRecieved(NetworkMessage TangoPhoneLastSavedRotMsg)
        {
            TangoPhoneLastSavedRotMessage msg = TangoPhoneLastSavedRotMsg.ReadMessage<TangoPhoneLastSavedRotMessage>();

            Quaternion lastSavedRot = msg.lastSavedRot;

            GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];
            tangoControllerParentInScene.gameObject.transform.rotation = lastSavedRot;
        }


        public void onTangoPhoneLocalPosMsgRecieved(NetworkMessage TangoPhoneLocalPosMSG)
        {
            TangoPhoneLocalPosMessage msg = TangoPhoneLocalPosMSG.ReadMessage<TangoPhoneLocalPosMessage>();

            Vector3 localPos = msg.localPos;

            //if (currentSpawnedMobileController != null)
            {
                //currentSpawnedMobileController.gameObject.transform.localPosition = localPos;
                //GameObject controllerInScene = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];
                //controllerInScene.gameObject.transform.localPosition = localPos;


                GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];
                tangoControllerInScene.gameObject.transform.position = localPos;

                GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];
                tangoControllerInScene.gameObject.transform.SetParent(tangoControllerParentInScene.gameObject.transform, true);

                GameObject controllerInScene = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];
                controllerInScene.gameObject.transform.localPosition = tangoControllerInScene.gameObject.transform.localPosition;

                tangoControllerInScene.gameObject.transform.SetParent(null, true);
                tangoControllerInScene.gameObject.transform.parent = null;







                //if (TangoAcceleroMeterZField)
                //{
                //    TangoAcceleroMeterZField.text = "x:" + tangoControllerInScene.gameObject.transform.position.x + 
                //                                    ",y:" + tangoControllerInScene.gameObject.transform.position.y + 
                //                                    ",z:" + tangoControllerInScene.gameObject.transform.position.z;
                //}
            }

        }

        public void onTangoPhoneActualRotMsgRecieved(NetworkMessage TangoPhoneActualRotMSG)
        {
            //Tangophon msg = TangoPhoneRotQueMSG.ReadMessage<TangoPhoneRotQueMessage>();
            TangoPhoneActualRotMessage msg = TangoPhoneActualRotMSG.ReadMessage<TangoPhoneActualRotMessage>();

            Quaternion actualRot = msg.actualRot;

            //if(currentSpawnedMobileController != null)
            {
                //currentSpawnedMobileController.gameObject.transform.localRotation = actualRot;
                GameObject controllerInScene = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];
                controllerInScene.gameObject.transform.localRotation = actualRot;

            }
        }



        Quaternion lastRecordedPhoneRotQua = new Quaternion();
        public void onTangoPhoneRotQueMsgRecieved(NetworkMessage TangoPhoneRotQueMSG)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                TangoPhoneRotQueMessage msg = TangoPhoneRotQueMSG.ReadMessage<TangoPhoneRotQueMessage>();

                Quaternion rot = msg.rotation;
                lastRecordedPhoneRotQua = msg.rotation;


                if (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length != 0 && isLocalPlayer)
                {
                    //GameObject currentMobileController = GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0];

                    //currentMobileController.gameObject.transform.rotation = rot;
                }
            }
        }

        public void onTangoPhoneStartedMsgRecieved(NetworkMessage TangoPhoneStartedMSG)
        {

            if (Application.platform != RuntimePlatform.Android)
            {
                TangoPhoneStartedMessage msg = TangoPhoneStartedMSG.ReadMessage<TangoPhoneStartedMessage>();
                bool phoneStarted = msg.phoneStarted;
                if (phoneStarted)
                {
                    TangoJoined = true;
                }

            }

        }


        float lastTimeSavedRotX = -99999f;
        float lastTimeSavedRotY = -99999f;
        float lastTimeSavedRotZ = -99999f;


        float recordedPhoneRotX = -99999f;
        float recordedPhoneRotY = -99999f;
        float recordedPhoneRotZ = -99999f;

        bool hasSavedRot = false; 


        public void onTangoPhoneRotMsgRecieved(NetworkMessage TangoPhoneRotMSG)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                TangoPhoneRotMessage msg = TangoPhoneRotMSG.ReadMessage<TangoPhoneRotMessage>();

                float x = msg.phoneRotX;
                float y = msg.phoneRotY;
                float z = msg.phoneRotZ;

                recordedPhoneRotX = x;
                recordedPhoneRotY = y;
                recordedPhoneRotZ = z;

                //if(!hasSavedRot)
                //{
                //    lastTimeSavedRotX = x;
                //    lastTimeSavedRotY = y;
                //    lastTimeSavedRotZ = z;

                //}


                if (mobileController)
                {




                    //Vector3 newRotation = new Vector3(x * 0.5f, y * 0.5f, z * 0.5f);
                    Vector3 newRotation = new Vector3(x, y, z);



                    //mobileController.gameObject.transform.eulerAngles = newRotation;

                    if (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length != 0)
                    {
                        //GameObject currentMobileController = GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0];

                        //float actualRotX = x - lastTimeSavedRotX;
                        //float actualRotY = y - lastTimeSavedRotY;
                        //float actualRotZ = z - lastTimeSavedRotZ;

                        //currentMobileController.gameObject.transform.Rotate(actualRotX, actualRotY, actualRotZ);
                        //currentMobileController.gameObject.transform.eulerAngles = newRotation;




                    }



                }
            }



        }



        public void onTangoPhonePosMsgRecieved(NetworkMessage TangoPhonePosMsgMSG)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                if (mobileController)
                {
                    TangoPhonePosMessage msg = TangoPhonePosMsgMSG.ReadMessage<TangoPhonePosMessage>();
                    float x = msg.phoneX;
                    float y = msg.phoneY;
                    float z = msg.phoneZ;



                    if (TangoAcceleroMeterZField)
                    {
                        TangoAcceleroMeterZField.text = "y" + y;
                    }

                    Vector3 currentLocalPos = mobileController.gameObject.transform.localPosition;

                    float newX = 0f;
                    float newY = 0f;
                    float newZ = 0f;

                    float xTrans = 0f;
                    float yTrans = 0f;
                    float zTrans = 0f;


                    float X_Acce = 0f;
                    float Y_Acce = 0f;
                    float Z_Acce = 0f;



                    if (currentX == -99999 && previousX == -99999)
                    {
                        currentX = x;
                        previousX = x;
                    }
                    else
                    {
                        previousX = currentX;
                        currentX = x;


                    }

                    if (currentY == -99999 && previousY == -99999)
                    {
                        currentY = y;
                        previousY = y;
                    }
                    else
                    {
                        previousY = currentY;
                        currentY = y;
                    }

                    if (currentZ == -99999 && previousZ == -99999)
                    {
                        currentZ = z;
                        previousZ = z;
                    }
                    else
                    {
                        previousZ = currentZ;
                        currentZ = z;
                    }




                    if (currentZ > previousZ )
                    {

                        zTrans = 0.01f;
                        //mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x, currentLocalPos.y, currentLocalPos.z + 0.01f);
                        newZ = currentLocalPos.z + 0.005f;
                        
                        if(currentZ - previousZ > 0.01f)
                        {
                            Z_Acce = 0.03f;
                        }
                        else
                        {
                            Z_Acce = 0f;

                        }

                        //Z_Acce =   
                    }
                    else if (currentZ < previousZ)
                    {
                        zTrans = -0.01f;

                        //mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x, currentLocalPos.y, currentLocalPos.z - 0.01f);
                        newZ = currentLocalPos.z - 0.005f;

                        if(previousZ - currentZ > 0.01f)
                        {
                            Z_Acce = -0.03f;

                        }
                        else
                        {
                            Z_Acce = 0f;

                        }


                    }
                    else
                    {
                        zTrans = 0f;
                        newZ = currentLocalPos.z;
                        Z_Acce = 0f;

                    }

                    if (currentY > previousY)
                    {
                        yTrans = 0.01f;
                        //mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x, currentLocalPos.y + 0.01f, currentLocalPos.z);

                        if (currentY - previousY > 0.01f)
                        {
                            newY = currentLocalPos.y + 0.005f;
                            Y_Acce = -0.03f;
                        }
                        else
                        {
                            Y_Acce = 0f;

                        }
                    }
                    else if (currentY < previousY)
                    {
                        //mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x, currentLocalPos.y - 0.01f, currentLocalPos.z);
                        yTrans = -0.01f;

                        if (previousY - currentY > 0.01f)
                        {
                            newY = currentLocalPos.y - 0.005f;
                            Y_Acce = 0.03f;
                        }
                        else
                        {
                            Y_Acce = 0f;
                        }

                    }
                    else
                    {
                        yTrans = 0f;
                        newY = currentLocalPos.y;
                        Y_Acce = 0f;
                    }



                    if (currentX > previousX)
                    {
                        xTrans = 0.01f;
                        //mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x + 0.01f, currentLocalPos.y, currentLocalPos.z);
                        if (currentX - previousX > 0.01f)
                        {
                            newX = currentLocalPos.x + 0.01f;
                            X_Acce = 0.03f;
                        }
                        else
                        {
                            X_Acce = 0f;
                        }
                    }
                    else if (currentX < previousX)
                    {
                        xTrans = -0.01f;
                        //mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x - 0.01f, currentLocalPos.y, currentLocalPos.z);
                        if (previousX - currentX > 0.01f)
                        {
                            newX = currentLocalPos.x - 0.01f;
                            X_Acce = -0.03f;
                        }
                        else
                        {
                            X_Acce = 0f;
                        }

                    }
                    else
                    {
                        xTrans = 0f;
                        newX = currentLocalPos.x;
                        X_Acce = 0f;

                    }


                    recordedPhonePosx = x;
                    recordedPhonePosy = y;
                    recordedPhonePosz = z;

                    float actualX = x - lastTimeSavedTangox;
                    float actualY = y - lastTimeSavedTangoy;
                    float actualZ = z - lastTimeSavedTangoz;

                    if (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length != 0)
                    {
                        GameObject currentMobileController = GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0];
                        //currentMobileController.gameObject.transform.Translate(new Vector3(X_Acce, Y_Acce, Z_Acce));
                        //currentMobileController.gameObject.transform.Translate(new Vector3(xTrans, yTrans, zTrans));


                        GameObject currentMobileControllerPrefab_flat = GameObject.FindGameObjectsWithTag("MobileControllerPrefab_flat")[0];

                        //currentMobileController.gameObject.transform.position =
                        //new Vector3(
                        //            currentMobileControllerPrefab_flat.gameObject.transform.position.x + actualX,
                        //            currentMobileControllerPrefab_flat.gameObject.transform.position.y + actualY,
                        //            currentMobileControllerPrefab_flat.gameObject.transform.position.z + actualZ
                        //            );

                        //currentMobileController.gameObject.transform.Translate(new Vector3(0, Y_Acce, 0));
                    }


                    //mobileController.gameObject.transform.Translate(new Vector3(X_Acce, Y_Acce, Z_Acce));

                    //float currentMobileController


                    //mobileController.gameObject.transform.localPosition = new Vector3(newX,newY,newZ);


                    //mobileController.gameObject.transform.Translate(actualX, actualY, actualZ);



                    //mobileController.gameObject.transform.localPosition =
                    //new Vector3(
                    //            0f + actualX,
                    //            0f + actualY,
                    //            0.572f + actualZ
                    //            );


                }
            }
        }





        GameObject raycastObject;
        float offsetX = 0f;
        float offsetY = 0f;
        float offsetZ = 0f;
        public GameObject cursorSphere;
        //Vector3 originalMobileControllerLocalPosition = new Vector3(0, 0, 0.572f);
        //public GameObject controller
        public void onTangoHoldingMsgRecieved(NetworkMessage TangoHoldingMSG)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                TangoHoldingMessage msg = TangoHoldingMSG.ReadMessage<TangoHoldingMessage>();
                bool isHolding = msg.holding;

                if (isHolding)
                {
                    if (raycastObject == null)
                    {
                        GameObject rayCastObj = GetComponent<NetworkedRayCast>().getRayCastObj();
                        if (rayCastObj != null)
                        {
                            raycastObject = rayCastObj;

                            //Vector3 mobileControllerCurrentWorldPos = mobileController.gameObject.transform.position;
                            //Quaternion mobileControllerCurrentWorldRotation = mobileController.gameObject.transform.rotation;


                            //mobileController.gameObject.transform.SetParent(null);
                            //mobileController.gameObject.transform.position = mobileControllerCurrentWorldPos;
                            //mobileController.gameObject.transform.rotation = mobileControllerCurrentWorldRotation;


                            if (cursorSphere)
                            {
                                cursorSphere.gameObject.SetActive(false);
                            }

                            //raycastObject.gameObject.transform.SetParent(mobileController.gameObject.transform);


                            GameObject currentMobileController = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];


                            Vector3 currentControllerWorldPos = currentMobileController.gameObject.transform.position;
                            offsetX = currentControllerWorldPos.x - raycastObject.gameObject.transform.position.x;
                            offsetY = currentControllerWorldPos.y - raycastObject.gameObject.transform.position.y;
                            offsetZ = currentControllerWorldPos.z - raycastObject.gameObject.transform.position.z;


                        }

                    }
                    else
                    {

                        if (cursorSphere)
                        {
                            cursorSphere.gameObject.SetActive(false);
                        }

                        GameObject currentMobileController = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];

                        Vector3 currentControllerWorldPos = currentMobileController.gameObject.transform.position;
                        Quaternion currentControllerWorldRot = currentMobileController.gameObject.transform.rotation;

                        float newX = currentControllerWorldPos.x - offsetX;
                        float newY = currentControllerWorldPos.y - offsetY;
                        float newZ = currentControllerWorldPos.z - offsetZ;
                        Vector3 newPos = new Vector3(newX, newY, newZ);
                        //raycastObject.transform.position.Set(newX, newY, newZ);
                        raycastObject.transform.position = newPos;
                        raycastObject.transform.rotation = currentControllerWorldRot;


                    }
                }
                else
                {
                    if (cursorSphere)
                    {
                        cursorSphere.gameObject.SetActive(true);
                    }

                    if (raycastObject != null)
                    {
                        //lastTimeSavedTangox = recordedPhonePosx;
                        //lastTimeSavedTangoy = recordedPhonePosy;
                        //lastTimeSavedTangoz = recordedPhonePosz;

                        //raycastObject.gameObject.transform.SetParent(null);
                        raycastObject = null;
                    }
                }
            }

        }


        public InputField TangoDoubleTappingField;
        float lastTimeSavedTangox = 0f;
        float lastTimeSavedTangoy = 0f;
        float lastTimeSavedTangoz = 0f;



        float recordedPhonePosx = 0f;
        float recordedPhonePosy = 0f;
        float recordedPhonePosz = 0f;

        public GameObject mobileControllerPrefab;
        public GameObject mobileControllerPrefab_2;
        public GameObject mobileControllerPrefab_flat;
        public GameObject mobileControllerRecabPrefab;



        public GameObject mobileControllerParentPrefab;
        //public GameObject spawnedControllerParent;
        GameObject mobileControllerParent;

        GameObject currentSpawnedMobileController;
        GameObject currentSpawnedMobileControllerPrefab;
        GameObject currentFlatController;
        //GameObject SCInstance;


        [Command]
        void CmdGetNewMobileControllerParent()
        {

        }


        [Command]
        void CmdGetNewController()
        {


            //Vector3 mobileControllerDir = transform.forward;
            //Vector3 mobileControllerDirPos = transform.position + mobileControllerDir * 1.5f;



            //GameObject nextBullet =
            //(GameObject)Instantiate(
            //    mobileControllerPrefab, 
            //    SharedCollection.Instance.gameObject.transform.InverseTransformPoint(mobileControllerDirPos),
            //    //mobileControllerDirPos,
            //    Quaternion.Euler(mobileControllerDir));


            //currentSpawnedMobileControllerPrefab =
            //(GameObject)Instantiate
            //(
            //    mobileControllerPrefab_flat,
            //    mobileController.gameObject.transform.position,
            ////new Quaternion()
            //    mobileController.gameObject.transform.rotation
            //);



            mobileControllerParent = (GameObject)Instantiate
            (
                mobileControllerParentPrefab
                //mobileController.gameObject.transform.position,
                ////lastRecordedPhoneRotQua
                ////new Quaternion()
                //mobileController.gameObject.transform.rotation
            );

            mobileControllerParent.gameObject.transform.position = mobileController.gameObject.transform.position;
            mobileControllerParent.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;


            //GameObject hololensControllerParent = GameObject.FindGameObjectsWithTag("HololensControllerParent")[0];
            //hololensControllerParent.gameObject.transform.position = mobileController.gameObject.transform.position;
            //hololensControllerParent.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;



            currentSpawnedMobileController =
            (GameObject)Instantiate
            (
                mobileControllerPrefab
                //mobileControllerParent.gameObject.transform
                //mobileController.gameObject.transform.position,
                //lastRecordedPhoneRotQua
                //new Quaternion()
                //mobileController.gameObject.transform.rotation
            );

            currentSpawnedMobileController.gameObject.transform.position = mobileControllerParent.gameObject.transform.position;
            currentSpawnedMobileController.gameObject.transform.rotation = mobileControllerParent.gameObject.transform.rotation;

            currentSpawnedMobileController.gameObject.transform.SetParent(mobileControllerParent.gameObject.transform);
            currentSpawnedMobileController.gameObject.transform.localPosition = new Vector3();
            currentSpawnedMobileController.gameObject.transform.localRotation = new Quaternion();

            //GameObject controller2 = (GameObject)Instantiate
            //(
            //    mobileControllerPrefab_2,
            //    mobileController.gameObject.transform.position,
            //    //lastRecordedPhoneRotQua
            //    //new Quaternion()
            //    mobileController.gameObject.transform.rotation
            //);


            //currentSpawnedMobileController.gameObject.transform.SetParent(mobileControllerParent.gameObject.transform);
            //currentSpawnedMobileController.gameObject.transform.localPosition = new Vector3();
            //currentSpawnedMobileController.gameObject.transform.localRotation = new Quaternion();

            //currentSpawnedMobileController = nextBullet;



        }

        [Command]
        void CmdGetNewController2()
        {
            GameObject actualController =
            (GameObject)Instantiate
            (
                mobileControllerPrefab_2,
                mobileControllerSpawnPos.gameObject.transform.position,
                mobileControllerSpawnPos.gameObject.transform.rotation
                //lastRecordedPhoneRotQua
            //new Quaternion()
            //mobileController.gameObject.transform.rotation
            );
        }

        [Command]
        void CmdGetNewControllerFlat()
        {
            currentFlatController =
            (GameObject)Instantiate
            (
                mobileControllerPrefab_flat,
                GameObject.FindGameObjectsWithTag("HololensControllerParent")[0].gameObject.transform
            //mobileControllerSpawnPos.gameObject.transform
            //mobileControllerSpawnPos.gameObject.transform.position,
            //mobileControllerSpawnPos.gameObject.transform.rotation
            //lastRecordedPhoneRotQua
            //new Quaternion()
            //mobileController.gameObject.transform.rotation
            );

            //flatController.gameObject.transform.SetParent(mobileControllerSpawnPos.gameObject.transform);
            //flatController.gameObject.transform.localPosition = new Vector3();
            //flatController.gameObject.transform.localRotation = new Quaternion();


        }


        [Command]
        void CmdDestroyOldController()
        {
            //Destroy(GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0], 0.1f);
            for(int i=0; i< GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length;i++ )
            {
                GameObject temp = GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[i];
                DestroyImmediate(temp);

            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("MobileControllerPrefab_2").Length; i++)
            {
                GameObject temp = GameObject.FindGameObjectsWithTag("MobileControllerPrefab_2")[i];
                DestroyImmediate(temp);

            }


            for (int i = 0; i < GameObject.FindGameObjectsWithTag("MobileControllerRecabPrefab").Length; i++)
            {
                GameObject temp = GameObject.FindGameObjectsWithTag("MobileControllerRecabPrefab")[i];
                DestroyImmediate(temp);

            }


            for (int i = 0; i < GameObject.FindGameObjectsWithTag("MobileControllerPrefab_flat").Length; i++)
            {
                GameObject temp = GameObject.FindGameObjectsWithTag("MobileControllerPrefab_flat")[i];
                DestroyImmediate(temp);

            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("MobileControllerParentPrefab").Length; i++)
            {
                GameObject temp = GameObject.FindGameObjectsWithTag("MobileControllerParentPrefab")[i];
                DestroyImmediate(temp);

            }




            //DestroyImmediate(GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0]);
        }

        [Command]
        void CmdDestroyOldController2()
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("MobileControllerPrefab_2").Length; i++)
            {
                GameObject temp = GameObject.FindGameObjectsWithTag("MobileControllerPrefab_2")[i];
                DestroyImmediate(temp);

            }
        }


        bool justRecievedDoubleTap = false;

        public void onTangoDoubleTapMsgRecieved(NetworkMessage TangoDoubleTapMSG)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                TangoDoubleTappingMessage msg = TangoDoubleTapMSG.ReadMessage<TangoDoubleTappingMessage>();
                bool isdoubleTapping = msg.doubleTapping;

                if (TangoDoubleTappingField)
                {
                    TangoDoubleTappingField.text = "DT: " + isdoubleTapping;
                }




                if (isdoubleTapping)
                {

                    GameObject tangoControllerParentInScene = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];
                    GameObject tangoControllerInScene = GameObject.FindGameObjectsWithTag("TangoController")[0];
                    tangoControllerParentInScene.gameObject.transform.position = tangoControllerInScene.gameObject.transform.position;



                    GameObject controllerInScene = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];
                    GameObject controllerParent = GameObject.FindGameObjectsWithTag("ControllerParent")[0];
                    controllerInScene.gameObject.transform.SetParent(null);
                    controllerInScene.gameObject.transform.parent = null;

                    //controllerParent.gameObject.transform.position = mobileControllerSpawnPos.gameObject.transform.position;
                    //controllerParent.gameObject.transform.rotation = mobileControllerSpawnPos.gameObject.transform.rotation;

                    controllerParent.gameObject.transform.position = this.transform.position;
                    controllerParent.gameObject.transform.rotation = this.transform.rotation;
                    //if (TangoAcceleroMeterZField)
                    //{
                    //    TangoAcceleroMeterZField.text = "x:" + transform.position.x +
                    //                                    "y:" + transform.position.y +
                    //                                    "z:" + transform.position.z;
                    //}

                    controllerParent.gameObject.transform.Translate(0, 0f, 0.5f);
                    //if (TangoAcceleroMeterZField)
                    //{
                    //    TangoAcceleroMeterZField.text = "x:" + controllerParent.gameObject.transform.position.x + 
                    //                                    "y:" + controllerParent.gameObject.transform.position.y +
                    //                                    "z:" + controllerParent.gameObject.transform.position.z;
                    //}
                    controllerInScene.gameObject.transform.position = controllerParent.gameObject.transform.position;
                    controllerInScene.gameObject.transform.rotation = controllerParent.gameObject.transform.rotation;
                    controllerInScene.gameObject.transform.SetParent(controllerParent.transform);
                    controllerInScene.gameObject.transform.localPosition = new Vector3();
                    controllerInScene.gameObject.transform.localRotation = new Quaternion();


                    //justRecievedDoubleTap = true;

                    //lastTimeSavedRotX = recordedPhoneRotX;
                    //lastTimeSavedRotY = recordedPhoneRotY;
                    //lastTimeSavedRotZ = recordedPhoneRotZ;

                    //lastTimeSavedTangox = recordedPhonePosx;
                    //lastTimeSavedTangoy = recordedPhonePosy;
                    //lastTimeSavedTangoz = recordedPhonePosz;

                    //previousZ = -99999;
                    //currentZ = -99999;
                    //previousX = -99999;
                    //currentX = -99999;
                    //previousY = -99999;
                    //currentY = -99999;
                    //offsetX = 0f;
                    //offsetY = 0f;
                    //offsetZ = 0f;
                    //raycastObject = null;

                    //if (isLocalPlayer && Application.platform != RuntimePlatform.Android)
                    //{
                    //    if (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length != 0)
                    //    {

                    //        currentSpawnedMobileController.gameObject.transform.SetParent(null);
                    //        currentSpawnedMobileController.gameObject.transform.parent = null;

                    //        mobileControllerParent.gameObject.transform.position = mobileController.gameObject.transform.position;
                    //        mobileControllerParent.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

                    //        currentSpawnedMobileController.gameObject.transform.position = mobileController.gameObject.transform.position;
                    //        currentSpawnedMobileController.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

                    //        currentSpawnedMobileController.gameObject.transform.SetParent(mobileControllerParent.gameObject.transform);
                    //        currentSpawnedMobileController.gameObject.transform.localPosition = new Vector3();
                    //        currentSpawnedMobileController.gameObject.transform.localRotation = new Quaternion();


                    //        ////Destroy(nextBullet, 8.0f);
                    //        ////CmdDestroyOldController2();
                    //        //CmdDestroyOldController();
                    //        //currentSpawnedMobileController = null;
                    //        //mobileControllerParent = null;
                    //        //currentSpawnedMobileControllerPrefab = null;


                    //    }
                    //}



                    //if (mobileControllerPrefab)
                    //{
                    //    if (isLocalPlayer && (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length == 0))
                    //    {
                    //        //CmdGetNewController2();
                    //        CmdGetNewController();

                    //        //GameObject hololensControllerParent = GameObject.FindGameObjectsWithTag("HololensControllerParent")[0];
                    //        //hololensControllerParent.gameObject.transform.position = mobileController.gameObject.transform.position;
                    //        //hololensControllerParent.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

                    //        //currentSpawnedMobileController.gameObject.transform.SetParent(hololensControllerParent.gameObject.transform);
                    //        //currentSpawnedMobileController.gameObject.transform.localPosition = hololensControllerParent.gameObject.transform.position
                    //        //currentSpawnedMobileController.gameObject.transform.localRotation = new Quaternion();

                    //    }
                    //}


                    //if (mobileController)
                    //{
                    //    lastTimeSavedTangox = recordedPhonePosx;
                    //    lastTimeSavedTangoy = recordedPhonePosy;
                    //    lastTimeSavedTangoz = recordedPhonePosz;


                    //    mobileController.gameObject.transform.SetParent(this.transform);
                    //    mobileController.gameObject.transform.localPosition = new Vector3(0, 0, 0.572f);
                    //    mobileController.gameObject.transform.localScale = new Vector3(0.05f, 0.01f, 0.25f);
                    //    mobileController.gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);


                    //    Vector3 mobileControllerCurrentWorldPos = mobileController.gameObject.transform.position;
                    //    Quaternion mobileControllerCurrentWorldRotation = mobileController.gameObject.transform.rotation;

                    //    mobileController.gameObject.transform.SetParent(null);
                    //    mobileController.gameObject.transform.position = mobileControllerCurrentWorldPos;
                    //    mobileController.gameObject.transform.rotation = mobileControllerCurrentWorldRotation;


                    //}
                }
            }
        }


        public GameObject mobileController;
        public GameObject mobileController_2;
        public GameObject mobileControllerSpawnPos;

        float previousZ = -99999;
        float currentZ = -99999;
        float previousX = -99999;
        float currentX = -99999;
        float previousY = -99999;
        float currentY = -99999;

        public InputField TangoAcceleroMeterZField;

        public void onTangoAccelerometerZMsgRecieved(NetworkMessage TangoAccelerometerZMSG)
        {
            //        transform.localPosition = new Vector3(0, 0, 0);
            //(0,0,0.572f)
            if (mobileController)
            {
                TangoAcceleroMeterZMessage msg = TangoAccelerometerZMSG.ReadMessage<TangoAcceleroMeterZMessage>();
                float z = msg.acceleroMeterZ;

                if (TangoAcceleroMeterZField)
                {
                    TangoAcceleroMeterZField.text = "z" + z.ToString();
                }

                Vector3 currentLocalPos = mobileController.gameObject.transform.localPosition;

                if (previousZ == -1 && currentZ == -1)
                {
                    previousZ = z;
                    currentZ = z;
                }
                else
                {
                    previousZ = currentZ;
                    currentZ = z;
                }

                if (currentZ > previousZ && currentZ - previousZ > 0.05f)
                {
                    mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x, currentLocalPos.y + 0.01f, currentLocalPos.z);
                }
                else if (currentZ < previousZ && previousZ - currentZ > 0.05f)
                {
                    mobileController.gameObject.transform.localPosition = new Vector3(currentLocalPos.x, currentLocalPos.y - 0.01f, currentLocalPos.z);

                }







            }
        }

        public InputField TangoGyroInfoField;
        public void onTangoGyroMsgRecieved(NetworkMessage TangoGyroMSG)
        {
            TangoGyroMessage msg = TangoGyroMSG.ReadMessage<TangoGyroMessage>();

            float x = msg.Gyrox;
            float y = msg.Gyroy;
            float z = msg.Gyroz;
            if (TangoGyroInfoField)
            {
                TangoGyroInfoField.text = "x: " + x + ", y: " + y + ", z:" + z;
            }


            GameObject TangoAsControllerTestCube = GameObject.FindGameObjectsWithTag("TangoAsControllerTestCube")[0];

            //TangoAsControllerTestCube.gameObject.transform.rotation.Set(x,y,z,0);

            TangoAsControllerTestCube.gameObject.transform.Rotate(x, y, z);


        }

        float previousPinchDistance = -1f;
        float currentPinchDistance = -1f;

        public InputField TangoPinchDistanceMessageField;
        float testScaleValue = 1f;
        //public GameObject TangoAsControllerTestCube;
        public void onTangoPinchDistanceMsgRecieved(NetworkMessage TangoPinchDistanceMSG)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                TangoPinchDistanceMessage msg = TangoPinchDistanceMSG.ReadMessage<TangoPinchDistanceMessage>();


                if (msg.pDistance == -1f)
                {
                    previousPinchDistance = -1f;
                    currentPinchDistance = -1f;

                }

                else
                {
                    if (previousPinchDistance == -1f && currentPinchDistance == -1f)
                    {
                        previousPinchDistance = msg.pDistance;
                        currentPinchDistance = msg.pDistance;

                    }
                    else
                    {
                        previousPinchDistance = currentPinchDistance;
                        currentPinchDistance = msg.pDistance;
                    }

                    if (currentPinchDistance >= previousPinchDistance)
                    {
                        testScaleValue = testScaleValue + 0.1f;
                    }
                    else
                    {
                        testScaleValue = testScaleValue - 0.1f;
                    }

                    if (testScaleValue >= 0.1f)
                    {

                        GameObject rayCastObj = GetComponent<NetworkedRayCast>().getRayCastObj();
                        if(rayCastObj!=null)
                        {
                            Vector3 scale = rayCastObj.gameObject.transform.localScale;
                            scale.Set(testScaleValue, testScaleValue, testScaleValue);
                            rayCastObj.gameObject.transform.localScale = scale;

                        }


                        //GameObject TangoAsControllerTestCube = GameObject.FindGameObjectsWithTag("TangoAsControllerTestCube")[0];

                        //Vector3 scale = TangoAsControllerTestCube.gameObject.transform.localScale;

                        //scale.Set(testScaleValue, testScaleValue, testScaleValue);

                        //TangoAsControllerTestCube.gameObject.transform.localScale = scale;


                    }
                }




                if (TangoPinchDistanceMessageField)
                {
                    TangoPinchDistanceMessageField.text = msg.pDistance.ToString();
                }
            }
        }

        public void onBugPaintingMsgRecieved(NetworkMessage bugpaintingMSG)
        {
            //BugPaintingMessage msg = bugpaintingMSG.ReadMessage<BugPaintingMessage>();

            //if(isLocalPlayer)
            //{
            //    if(msg.erasing)
            //    {

            //        Vector3 temp = new Vector3(msg.paintPosX, msg.paintPosY, msg.paintPosZ);
            //        GetComponent<networkedShaderUniformUpdate>().addfaceDrawingPoint(temp);
            //    }
            //    if(msg.painting)
            //    {
            //        Vector3 temp = new Vector3(msg.paintPosX, msg.paintPosY, msg.paintPosZ);
            //        GetComponent<networkedShaderUniformUpdate>().removeFaceDrawingPoint(temp);
            //    }
            //}


        }

        public void OnMyPointerPositionRecieved(NetworkMessage myPointerPosMsg)
        {
            //MyPointerAtBugPositionMessage msg = myPointerPosMsg.ReadMessage<MyPointerAtBugPositionMessage>();
            //if(isServer && isClient)
            //{
            //    if(msg.requestMsgFrom != "SERVER")
            //    {

            //        Vector3 temp = new Vector3(msg.myPointerPosX, msg.myPointerPosY, msg.myPointerPosZ);
            //        GetComponent<networkedShaderUniformUpdate>().setOtherPointerPosition(temp);
            //    }
            //}

            //if(!isServer && isClient)
            //{
            //    if(msg.requestMsgFrom != "CLIENT")
            //    {
            //        Vector3 temp = new Vector3(msg.myPointerPosX, msg.myPointerPosY, msg.myPointerPosZ);
            //        GetComponent<networkedShaderUniformUpdate>().setOtherPointerPosition(temp);
            //    }
            //}
        }

        public void OnShowNextBug(NetworkMessage showNextBugMsg)
        {
            //if(isLocalPlayer)
            //{

            //    if(yesToNextButton)
            //    {
            //        yesToNextButton.gameObject.SetActive(false);
            //    }

            //    if(beginMsgTransmissionButton)
            //    {
            //        beginMsgTransmissionButton.gameObject.SetActive(false);
            //    }

            //    if(nextBugRequestButton)
            //    {
            //        nextBugRequestButton.gameObject.SetActive(false);
            //    }



            //    GameObject.FindGameObjectsWithTag("FileReader")[0].GetComponent<testConnect>().
            //        setCurrentModelURLS("position", "normal", "uv", "hi", "");

            //    GetComponent<networkedShaderUniformUpdate>().enabled = true;

            //    modelBuilt = true;
            //}
        }

        public void OnAgreeNextBug(NetworkMessage nextBugRequestMsg)
        {
            AgreeNextBugMessage msg = nextBugRequestMsg.ReadMessage<AgreeNextBugMessage>();
            string requestFromName = msg.requestMsgFrom;
            if (isServer && isClient)
            {
                numberOfPeopleAgreeNextBug++;
            }
            if (isServer && isClient)
            {

                if (numberOfPeopleAgreeNextBug >= 2)
                {
                    allPeopleAgreedNextBug = true;
                }
            }


        }

        bool allPeopleAgreedNextBug = false;
        int numberOfPeopleAgreeNextBug = 0;
        public Button yesToNextButton;
        public Button nextBugRequestButton;
        public Button beginMsgTransmissionButton;
        public void OnNextBugRequest(NetworkMessage nextBugRequestMsg)
        {
            //if(isServer)
            //{
            //    numberOfPeopleAgreeNextBug++;
            //}
            NextBugRequestMessage msg = nextBugRequestMsg.ReadMessage<NextBugRequestMessage>();
            string requestFromName = msg.requestMsgFrom;
            if (yesToNextButton && isLocalPlayer)
            {
                if (beginMsgTransmissionButton)
                {
                    beginMsgTransmissionButton.gameObject.SetActive(false);
                }
                if (nextBugRequestButton)
                {
                    nextBugRequestButton.gameObject.SetActive(false);
                }
                yesToNextButton.gameObject.SetActive(true);


                if (requestMsgField)
                {
                    //requestMsgField.text = "SERVER GOT: " + requestMSG + " FROM: " + requestFromName;
                    requestMsgField.text = msg.requestMsgRecieved;
                }
            }

        }

        public InputField requestMsgField;
        public InputField anchorStatusField;
        public void OnRequest(NetworkMessage requestMsg)
        {
            RequestMessage msg = requestMsg.ReadMessage<RequestMessage>();
            string requestMSG = msg.requestMsgRecieved;
            string requestFromName = msg.requestMsgFrom;
            if (requestMsgField && isLocalPlayer)
            {

                if (isServer && isClient)
                {
                    if (requestFromName != "SERVER")
                    {
                        requestMsgField.text = "SERVER GOT: " + requestMSG + " FROM: " + requestFromName;
                    }
                }
                else if (!isServer && isClient)
                {
                    if (requestFromName != "CLIENT")
                    {
                        requestMsgField.text = "CLIENT GOT: " + requestMSG + " FROM: " + requestFromName;
                    }
                }
            }
        }

        public void OnScore(NetworkMessage netMsg)
        {
            //Byte[] b = netMsg.ReadMessage<Byte[]>();
            ChatMessage msg = netMsg.ReadMessage<ChatMessage>();
            //Debug.Log("OnScoreMessage " + msg.score);
            //if (isLocalPlayer)
            //{
            //            inputField.text = msg.msgRecieved;
            //if (isLocalPlayer)
            // {
            //    myChatBox.text += "\n" + msg.msgFromName+" : "+msg.msgRecieved;

            //_image.rectTransform.sizeDelta = new Vector2(width, height);

            //chatImage.rectTransform.sizeDelta = new Vector2(chatImage.rectTransform.sizeDelta.x, chatImage.rectTransform.sizeDelta.y+14f);
            int oneline = 22;
            string addtext = msg.msgFromName + " : " + msg.msgRecieved;
            int length = addtext.Length;
            int addnewlines = length / oneline;

            int linenumber = addnewlines == 0 ? 1 : addnewlines;

            chatText.text += "\n" + msg.msgFromName + " : " + msg.msgRecieved;
            chatText.rectTransform.sizeDelta = new Vector2(chatText.rectTransform.sizeDelta.x, chatText.rectTransform.sizeDelta.y + (linenumber + 0.5f) * 16f);
            scrollbar.value = 0f;
            //scrollChatField.text += "\n" + msg.msgFromName + " : " + msg.msgRecieved;
            // }
            // }
            Debug.Log("Player: " + this.netId + " got msg! FROM: " + msg.msgFrom);

        }

        public void OnConnected(NetworkMessage netMsg)
        {
            Debug.Log("Connected to server");
        }

        public void OnChangeCurrentMsg(string current)
        {
            Debug.Log(isLocalPlayer + " : " + current);
            //currentMsg = current;
            //myChatBox.text = current;

            chatText.text = current;

            //chatRoom cr = chatRooms[0].GetComponent<chatRoom>();
            //myChatBox.text = cr.getLastHistory();
            //GameObject[] playersONLINE = GameObject.FindGameObjectsWithTag("Player");

            //for (int i = 0; i < playersONLINE.Length; i++)
            //{
            //    chat chatScript = playersONLINE[i].GetComponent<chat>();
            //    chatScript.updateChatBox(current + " "+ playersONLINE.Length +" players");


            //}

        }

        public void updateChatBox(string newmsg)
        {
            myChatBox.text = newmsg;
        }

        void Start()
        {

            if (anchorStatusField && isLocalPlayer)
            {
                
                anchorStatusField.text = "ANCHOR EXPORTED/IMPORTED, CHAT ACTIVATED";
            }

            //if (SharedCollection.Instance)
            //{

            //}

            //if(mobileController)
            //{
            //    Vector3 currentMobileControllerWorldPosition = mobileController.gameObject.transform.position;
            //    Quaternion currentMobileControllerWorldRotation = mobileController.gameObject.transform.rotation;

            //    mobileController.gameObject.transform.SetParent(null);
            //    mobileController.gameObject.transform.position = currentMobileControllerWorldPosition;
            //    mobileController.gameObject.transform.rotation = currentMobileControllerWorldRotation;



            //}


            // chatText.rectTransform.sizeDelta = new Vector2(chatText.rectTransform.sizeDelta.x, chatText.rectTransform.sizeDelta.y + 14f);
            // Button btn = sendButton.GetComponent<Button>();
            // btn.onClick.AddListener(TaskOnClick);

            //Application.platform.ToString();
            //btn.onClick.
            //SetupClient();
        }


        public void setNextBugRequestButtonPressed()
        {
            if (isLocalPlayer)
            {
                nextBugRequestButtonPressed = true;
            }
        }

        public void setAgreeBugRequestButtonPressed()
        {
            if (isLocalPlayer)
            {
                agreeNextBugButtonPressed = true;
            }
        }

        bool clientSetupFinished = false;
        bool nextBugRequestButtonPressed = false;
        bool agreeNextBugButtonPressed = false;
        bool modelBuilt = false;



        bool TangoJoined = false;
        bool mobileControllerDeparented = false;
        public InputField mobileControllerNumField;
        bool TangoPhoneParentChildSet = false;

        //bool 
        // Update is called once per frame
        void Update()
        {

            if (!clientSetupFinished && Application.platform != RuntimePlatform.Android && isLocalPlayer)
            {
                if (Application.platform != RuntimePlatform.Android && isLocalPlayer)
                {
                    SetupClient();
                    //GameObject controllerParent = GameObject.FindGameObjectsWithTag("ControllerParent")[0];
                    //GameObject controllerInScene = GameObject.FindGameObjectsWithTag("ControllerInScene")[0];
                    //controllerInScene.gameObject.SetActive(true);



                    ////controllerParent.gameObject.transform.position = mobileControllerSpawnPos.gameObject.transform.position;
                    ////controllerParent.gameObject.transform.rotation = mobileControllerSpawnPos.gameObject.transform.rotation;
                    //controllerParent.gameObject.transform.position = transform.position;
                    //controllerParent.gameObject.transform.rotation = transform.rotation;
                    //controllerParent.gameObject.transform.Translate(0, 0, 0.572f);//0,0,0.572f

                    //controllerInScene.gameObject.transform.position = controllerParent.gameObject.transform.position;
                    //controllerInScene.gameObject.transform.rotation = controllerParent.gameObject.transform.rotation;

                    //controllerInScene.gameObject.transform.SetParent(controllerParent.gameObject.transform);
                    //controllerInScene.gameObject.transform.localPosition = new Vector3();
                    //controllerInScene.gameObject.transform.localRotation = new Quaternion();


                    //Transform localPlayerTransform = this.transform;


                    clientSetupFinished = true;
                }
            }

            //if (TangoJoined)
            //{
            //    GameObject ControllerParent = GameObject.FindGameObjectsWithTag("ControllerParent")[0];
            //    GameObject TangoControllerParent = GameObject.FindGameObjectsWithTag("TangoControllerParent")[0];

            //    TangoControllerParent.gameObject.transform.position = ControllerParent.gameObject.transform.position;
            //    TangoControllerParent.gameObject.transform.rotation = ControllerParent.gameObject.transform.rotation;

            //}



            //if(TangoPhoneParentChildSet == false && TangoJoined)
            //{

            //}

            //if (clientSetupFinished)
            //{
            //    CmdSendTangoDoubleTapping(true);
            //}
            //if(mobileControllerNumField)
            //{
            //    if (isLocalPlayer)
            //    {
            //        mobileControllerNumField.text = "NumOfMobileController: " + GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length;
            //    }
            //}

            //if (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length == 0)
            //{
            //    if (isLocalPlayer && Application.platform != RuntimePlatform.Android)
            //    {

            //        //GameObject hololensControllerParent = GameObject.FindGameObjectsWithTag("HololensControllerParent")[0];
            //        //hololensControllerParent.gameObject.transform.position = mobileController.gameObject.transform.position;
            //        //hololensControllerParent.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

            //        CmdGetNewController();

            //        //currentFlatController.gameObject.transform.SetParent(hololensControllerParent.gameObject.transform);
            //        //currentFlatController.gameObject.transform.localPosition = new Vector3();
            //        //currentFlatController.gameObject.transform.localRotation = new Quaternion();
            //    }
            //}

            //if (isLocalPlayer && Application.platform != RuntimePlatform.Android && justRecievedDoubleTap)
            //{
            //    if (GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length != 0)
            //    {

            //        currentSpawnedMobileController.gameObject.transform.SetParent(null);
            //        currentSpawnedMobileController.gameObject.transform.parent = null;

            //        mobileControllerParent.gameObject.transform.position = mobileController.gameObject.transform.position;
            //        mobileControllerParent.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

            //        currentSpawnedMobileController.gameObject.transform.position = mobileController.gameObject.transform.position;
            //        currentSpawnedMobileController.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

            //        currentSpawnedMobileController.gameObject.transform.SetParent(mobileControllerParent.gameObject.transform);
            //        currentSpawnedMobileController.gameObject.transform.localPosition = new Vector3();
            //        currentSpawnedMobileController.gameObject.transform.localRotation = new Quaternion();

            //        justRecievedDoubleTap = false;
            //    }
            //}






            //if (TangoAcceleroMeterZField && mobileController)
            //{
            //    TangoAcceleroMeterZField.text = "x:" + mobileController.gameObject.transform.position.x + ",y:" + mobileController.gameObject.transform.position.y + ",z:" + mobileController.gameObject.transform.position.z;
            //}


            //else
            //{
            //    if (isLocalPlayer)
            //    {
            //        GameObject MobileControllerFlat = GameObject.FindGameObjectsWithTag("MobileControllerPrefab_flat")[0];
            //        MobileControllerFlat.gameObject.transform.position = mobileController.gameObject.transform.position;
            //        MobileControllerFlat.gameObject.transform.rotation = mobileController.gameObject.transform.rotation;

            //    }
            //}


            //if (Gameobject.findgameobjectswithtag("mobilecontrollerprefab").length == 0 && tangojoined)
            //{
            //    if (islocalplayer && application.platform != runtimeplatform.android)
            //    {
            //        cmdgetnewcontroller();
            //    }
            //}

            //if(GameObject.FindGameObjectsWithTag("MobileControllerPrefab").Length != 0)
            //{

            //    GameObject referenceMobileController = GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0];
            //    recordedPhoneRotX = referenceMobileController.gameObject.transform.eulerAngles.x;
            //    recordedPhoneRotY = referenceMobileController.gameObject.transform.eulerAngles.y;
            //    recordedPhoneRotZ = referenceMobileController.gameObject.transform.eulerAngles.z;



            //}



            //else
            //{
            //    if(isLocalPlayer && Application.platform != RuntimePlatform.Android)
            //    {
            //        currentSpawnedMobileController = GameObject.FindGameObjectsWithTag("MobileControllerPrefab")[0];
            //        Vector3 pos = mobileController.gameObject.transform.position;
            //        Quaternion rot = mobileController.gameObject.transform.rotation;
            //        currentSpawnedMobileController.gameObject.transform.position = pos;
            //        currentSpawnedMobileController.gameObject.transform.rotation = rot;
            //    } 
            //}

            //if (TangoJoined && Application.platform != RuntimePlatform.Android)
            //{
            //    if (!mobileControllerDeparented)
            //    {
            //        if (isLocalPlayer)
            //        {
            //            if (mobileController)
            //            {
            //                Vector3 currentMobileControllerWorldPosition = mobileController.gameObject.transform.position;
            //                Quaternion currentMobileControllerWorldRotation = mobileController.gameObject.transform.rotation;

            //                mobileController.gameObject.transform.SetParent(null);
            //                mobileController.gameObject.transform.position = currentMobileControllerWorldPosition;
            //                mobileController.gameObject.transform.rotation = currentMobileControllerWorldRotation;
            //                mobileControllerDeparented = true;
            //            }
            //        }
            //    }
            //}



            //if (Input.GetKeyDown(KeyCode.KeypadEnter)  || Input.GetKeyDown(KeyCode.Return))
            //{
            //    if (isLocalPlayer)
            //    {
            //        if (inputField.text != "")
            //        {
            //            CmdBLC(inputField.text, nameField.text);
            //        }
            //        inputField.text = "";
            //        inputField.Select();
            //        inputField.ActivateInputField();



            //    }
            //}
            //<------------------------ABOVE FROM PREVIOUS NETWORKTEST----------------------->

            //if (Application.platform != RuntimePlatform.Android)//on Hololens
            //{
            //    if (isLocalPlayer && clientSetupFinished && nextBugRequestButtonPressed)
            //    {
            //        string name = "";
            //        if (isServer && isClient)
            //        {
            //            name = "SERVER";
            //        }
            //        else if (!isServer && isClient)
            //        {
            //            name = "CLIENT";
            //        }
            //        CmdSendNextBugRequest("NEXT BUG?", name);
            //        nextBugRequestButtonPressed = false;
            //        //    //CmdSendRequest("NEXT BUG?", name);

            //    }

            //    if (isLocalPlayer && clientSetupFinished && agreeNextBugButtonPressed)
            //    {
            //        string name = "";
            //        if (isServer && isClient)
            //        {
            //            name = "SERVER";
            //        }
            //        else if (!isServer && isClient)
            //        {
            //            name = "CLIENT";
            //        }
            //        CmdSendAgreeNextBug(true, name);
            //        agreeNextBugButtonPressed = false;

            //    }

            //    if (isLocalPlayer && clientSetupFinished && allPeopleAgreedNextBug)
            //    {
            //        if (isServer && isClient)
            //        {
            //            string name = "";
            //            if (isServer && isClient)
            //            {
            //                name = "SERVER";
            //            }
            //            else if (!isServer && isClient)
            //            {
            //                name = "CLIENT";
            //            }

            //            CmdSendShowNextBug("shownextbug", name);
            //        }
            //        allPeopleAgreedNextBug = false;

            //    }

            //    if (isLocalPlayer && clientSetupFinished && modelBuilt)
            //    {
            //        string name = "";
            //        if (isServer && isClient)
            //        {
            //            name = "SERVER";
            //        }
            //        else if (!isServer && isClient)
            //        {
            //            name = "CLIENT";
            //        }

            //        CmdSendMyPointerPositionMsg(GetComponent<networkedShaderUniformUpdate>().getMyPointerPosition(), name);
            //    }

            //    if (isLocalPlayer && clientSetupFinished && gotBugPaintingMessageToSend)
            //    {
            //        string name = "";
            //        if (isServer && isClient)
            //        {
            //            name = "SERVER";
            //        }
            //        else if (!isServer && isClient)
            //        {
            //            name = "CLIENT";
            //        }

            //        CmdSendBugPaintingMsg(paintPointPassedIn, name, paintPointIsDrawing, paintPointIsErasing);

            //    }

            //}




#if !UNITY_WSA_10_0
            //SCALE VALUE


            float scaleValue = GetComponent<NetworkedDetectTouchMovement>().getPinchDistance();
            bool isPinching = GetComponent<NetworkedDetectTouchMovement>().ifPinching();
            if(scaleValue > 0 && isPinching)
            {

                CmdSendTangoPinchDistance(scaleValue);
                if(TangoPinchDistanceMessageField)
                {
                    TangoPinchDistanceMessageField.text = scaleValue.ToString();
                }
            }
            else
            {

                CmdSendTangoPinchDistance(-1);

                if (TangoPinchDistanceMessageField)
                {
                    TangoPinchDistanceMessageField.text = "no pinching/not valid pinch value";
                }
            }
            //ROTATION VALUE
            //float rotationValueX = GetComponent<networkedDetectDeviceRotation>().getRotX();
            //float rotationValueY = GetComponent<networkedDetectDeviceRotation>().getRotY();
            //float rotationValueZ = GetComponent<networkedDetectDeviceRotation>().getRotZ();

            //CmdSendTangoGyroRotation(rotationValueX, rotationValueY, rotationValueZ);
            
            //if(TangoGyroInfoField)
            //{
            //    TangoGyroInfoField.text = "x: " + rotationValueX + "y: " + rotationValueY + "z: " + rotationValueZ;
            //}

            //GET LONG TOUCH BOOL
            bool isHolding = GetComponent<NetworkedDetectLongTouch>().getIsHolding();
            CmdSendTangoHolding(isHolding);


            //GET DOUBLE TAP
            bool isDoubleTapping = GetComponent<NetworkedDoubleTap>().getIsDoubleTapping();
            CmdSendTangoDoubleTapping(isDoubleTapping);


        //GET ACCELEROMETERZ
        float acceY = GetComponent<NetworkedPhoneMovement>().getY();
        float acceX = GetComponent<NetworkedPhoneMovement>().getX();
        float acceZ = GetComponent<NetworkedPhoneMovement>().getZ();
        //CmdSendTangoPhonePos(acceX, acceY, acceZ);
            float phoneZ = GetComponent<NetworkedPhoneMovement>().getPhoneZ();
            float phoneX = GetComponent<NetworkedPhoneMovement>().getPhoneX();
            float phoneY = GetComponent<NetworkedPhoneMovement>().getPhoneY();
            //CmdSendTangoPhonePos(phoneX, phoneY, phoneZ);
            //CmdSendTangoAccelerometerZ(acceZ);



            //GET PHONE ROTATION
        float phoneRotX = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotX();
        float phoneRotY = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotY();
        float phoneRotZ = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotZ();
        float phoneRotEulerAngleX = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotEulerAngleX();
        float phoneRotEulerAngleY = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotEulerAngleY();
        float phoneRotEulerAngleZ = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotEulerAngleZ();
        Quaternion phoneRot = GetComponent<NetworkedPhoneRotation>().getTangoPhoneRotation();
            //CmdSendTangoRotQua(phoneRot);
            //CmdSendTangoPhoneRot(phoneRotX, phoneRotY, phoneRotZ);
            //CmdSendTangoPhoneRot(phoneRotEulerAngleX, phoneRotEulerAngleY, phoneRotEulerAngleZ);


            //GET TANGO CONNECTED
            bool TangoStarted = GetComponent<TangoConnection>().getTangoConnected();
            CmdSendTangoStarted(TangoStarted);
            //GET TANGO ACUTAL ROT
            Quaternion actualRot = GetComponent<TangoRotationRecorder>().getActualRot();
            CmdSendTangoActualRot(actualRot);
            //GET TANGO LAST SAVED ROT
            Quaternion lastSavedRot = GetComponent<TangoRotationRecorder>().getLastSavedRot();
            CmdSendTangolastSavedRot(lastSavedRot);
            //GET TANGO LAST SAVED ROT
            Quaternion lastRecordedRot = GetComponent<TangoRotationRecorder>().getLastRecordedRot();
            CmdSendTangolastRecordedRot(lastRecordedRot);

            //GET TANGO JUST RESET
            bool justReset = GetComponent<TangoRotationRecorder>().getJustReset();
            CmdSendTangoJustReset(justReset);
            //GET TANGO LOCAL POS
            Vector3 localPos = GetComponent<TangoPositionRecorder>().getLastRecordedPos();
            CmdSendTangoLocalPos(localPos);



#endif






        }


        [Command]
        public void CmdSendTangolastRecordedRot(Quaternion lastRecordedRot)
        {
            RpcSendTangolastRecordedRot(lastRecordedRot);
        }
        [ClientRpc]
        public void RpcSendTangolastRecordedRot(Quaternion lastRecordedRot)
        {
            sendTangoLastRecordedRot(lastRecordedRot);
        }

        [Command]
        public void CmdSendTangolastSavedRot(Quaternion lastSavedRot)
        {
            RpcSendTangolastSavedRot(lastSavedRot);
        }
        [ClientRpc]
        public void RpcSendTangolastSavedRot(Quaternion lastSavedRot)
        {
            sendTangoLastSavedRot(lastSavedRot);
        }


        [Command]
        public void CmdSendTangoJustReset(bool justReset)
        {
            RpcSendTangoJustReset(justReset);
        }

        [ClientRpc]
        public void RpcSendTangoJustReset(bool justReset)
        {
            sendTangoJustReset(justReset);
        }


        [Command]
        public void CmdSendTangoLocalPos(Vector3 localPos)
        {
            RpcSendTangoLocalPos(localPos);
        }

        [ClientRpc]
        public void RpcSendTangoLocalPos(Vector3 localPos)
        {
            sendTangoPhoneLocalPos(localPos);

        }

        [Command]
        public void CmdSendTangoActualRot(Quaternion actualRot)
        {
            RpcSendTangoActualRot(actualRot);
        }

        [ClientRpc]
        public void RpcSendTangoActualRot(Quaternion actualRot)
        {
            sendTangoPhoneActualRot(actualRot);

        }



        [Command]
        public void CmdSendTangoRotQua(Quaternion rotqua)
        {
            RpcSendTangoRotQua(rotqua);
        }

        [ClientRpc]
        public void RpcSendTangoRotQua(Quaternion rotqua)
        {
            sendTangoPhoneRotQue(rotqua);

        }



        [Command]
        public void CmdSendTangoStarted(bool TangoStarted)
        {
            RpcSendTangoStarted(TangoStarted);
        }

        [ClientRpc]
        public void RpcSendTangoStarted(bool TangoStarted)
        {
            sendTangoStarted(TangoStarted);
        }



        [Command]
        public void CmdSendTangoPhoneRot(float x, float y, float z)
        {
            RpcSendTangoPhoneRot(x, y, z);
        }

        [ClientRpc]
        public void RpcSendTangoPhoneRot(float x, float y, float z)
        {
            sendTangoPhoneRot(x, y, z);
        }




        [Command]
        public void CmdSendTangoPhonePos(float x, float y, float z)
        {
            RpcSendTangoPhonePos(x, y, z);
        }

        [ClientRpc]
        public void RpcSendTangoPhonePos(float x, float y, float z)
        {
            sendTangoPhonePos(x, y, z);
        }



        [Command]
        public void CmdSendTangoHolding(bool holding)
        {
            RpcSendTangoHolding(holding);
        }

        [ClientRpc]
        public void RpcSendTangoHolding(bool holding)
        {
            sendTangoholding(holding);
        }




        [Command]
        public void CmdSendTangoDoubleTapping(bool doubleTapping)
        {
            RpcSendTangoDoubleTapping(doubleTapping);
        }

        [ClientRpc]
        public void RpcSendTangoDoubleTapping(bool doubleTapping)
        {
            sendTangoDoubleTapping(doubleTapping);
        }


        [Command]
        public void CmdSendTangoAccelerometerZ(float z)
        {
            RpcSendTangoAccelerometerZ(z);
        }

        [ClientRpc]
        public void RpcSendTangoAccelerometerZ(float z)
        {
            sendTangoAccelerometerZ(z);
        }



        [Command]
        public void CmdSendTangoGyroRotation(float rotX, float rotY, float rotZ)
        {
            RpcSendTangoGyroRotation(rotX, rotY, rotZ);
        }

        [ClientRpc]
        public void RpcSendTangoGyroRotation(float rotX, float rotY, float rotZ)
        {
            sendTangoGyroRotation(rotX, rotY, rotZ);
        }



        [Command]
        public void CmdSendTangoPinchDistance(float tangoPinchD)
        {
            RpcSendTangoPinchDistance(tangoPinchD);
        }

        [ClientRpc]
        public void RpcSendTangoPinchDistance(float tangoPinchD)
        {
            sendTangoPinchDistance(tangoPinchD);
        }



        [Command]
        public void CmdSendBugPaintingMsg(Vector3 pos, string name, bool drawing, bool erasing)
        {
            RpcSendBugPaintingMsg(pos, name, drawing, erasing);
        }

        [ClientRpc]
        public void RpcSendBugPaintingMsg(Vector3 pos, string name, bool drawing, bool erasing)
        {
            sendBugPainting(pos, name, drawing, erasing);
        }


        [Command]
        public void CmdSendMyPointerPositionMsg(Vector3 pos, string name)
        {
            RpcSendMyPointerPos(pos, name);
        }

        [ClientRpc]
        public void RpcSendMyPointerPos(Vector3 pos, string name)
        {
            sendMyPointerPosition(pos, name);
        }



        [Command]
        public void CmdSendShowNextBug(string msg, string name)
        {
            RpcSendShowNextBug(msg, name);
        }

        [ClientRpc]
        public void RpcSendShowNextBug(string msg, string name)
        {
            sendShowNextBug(msg, name);
        }

        [Command]
        public void CmdSendAgreeNextBug(bool agreeOrNot, string name)
        {
            RpcSendAgreeNextBug(agreeOrNot, name);
        }

        [ClientRpc]
        public void RpcSendAgreeNextBug(bool agreeOrNot, string name)
        {
            sendAgreeNextBug(agreeOrNot, name);
        }


        [Command]
        public void CmdSendNextBugRequest(string requestmsg, string name)
        {
            RpcSendNextBugRequest(requestmsg, name);
        }

        [ClientRpc]
        public void RpcSendNextBugRequest(string requestmsg, string name)
        {
            sendNextBugRequest(requestmsg, name);
        }

        [Command]
        void CmdSendRequest(string requestmsg, string name)
        {
            RpcSendRequest(requestmsg, name);
        }

        [ClientRpc]
        void RpcSendRequest(string requestmsg, string name)
        {
            sendRequest(requestmsg, name);
        }

        [Command]
        void CmdBLC(string chatmsg, string name)
        {
            RpcBreakLightControl(chatmsg, name);
        }

        [ClientRpc]
        void RpcBreakLightControl(string chatmsg, string name)
        {
            //myChatBox.text = chatmsg;
            SendScore(chatmsg, name);
            //CmdChange();

        }

        [Command]
        void CmdChange()
        {
            change();
        }

        //[ClientRpc]
        //void RpcShowBoard()
        //{
        //    msgBoard
        //}

        void change()
        {


            //GameObject[] chatRooms = GameObject.FindGameObjectsWithTag("chatRoom");


            ////if (isClient)
            ////{
            //if (chatRooms.Length != 0)
            //{

            //    for (int i = 0; i < chatRooms.Length; i++)
            //    {
            //        chatRoom cr = chatRooms[i].GetComponent<chatRoom>();

            //        string msg = inputField.text;

            //        cr.updateChatHistory(msg);





            //    }
            //}
            //else
            //{


            //    inputField.text = "Can't find chat room instance :-(";
            //}
        }


    }
}