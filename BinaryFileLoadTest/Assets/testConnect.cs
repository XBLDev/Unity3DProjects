using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class testConnect : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextIndicator;
    public Transform TextLoading;
    public Canvas LoadingCanvas; 

    public Text posText;
    public Text normalText;
    public Text UVText;
    public Text HiText;
    public Text annotationText;
    public Text positionFirstLineText;

    //public Text textBoard;


    private string urlPosition = "https://someURL/services/p3d-legacy/models/2bhqPqmKn8LEjTJY2/geometry/position";
    private string urlNormal = "https://someURL/services/p3d-legacy/models/2bhqPqmKn8LEjTJY2/geometry/normal";
    private string urlUV = "https://someURL/services/p3d-legacy/models/2bhqPqmKn8LEjTJY2/geometry/uv";
    private string urlHi = "https://someURL/services/p3d-legacy/models/2bhqPqmKn8LEjTJY2/texture/hi";
    private string urlAnnotations = "https://someURL/services/p3d-legacy/models/2bhqPqmKn8LEjTJY2/annotations​";

    private string currentUrlPosition;
    private string currentUrlNormal;
    private string currentUrlUV;
    private string currentUrlHi;
    private string currentUrlAnnotations;

    string positionFileName = "postition.txt";
    private int meshVertexLimit = 65532;//make sure vertex number is a multiply of 3
    private int meshTestVertexCount = 65532;

    private Mesh testMesh;



    private Vector3[] positions;
    private List<Vector3[]> positionList;

    private int[] triangles;
    private List<int[]> triangleList;

    private Vector3[] normals;
    private List<Vector3[]> normalList;

    private Vector2[] uv;
    private List<Vector2[]> uvList;


    private Color[] vertexColors;
    //private List<Vector3[]> ColorList;




    float maxX = 0f;
    float minX = 0f;
    float maxY = 0f;
    float minY = 0f;
    float maxZ = 0f;
    float minZ = 0f;
    float centerX = 0f;
    float centerY = 0f;
    float centerZ = 0f;
    bool firstX = true;
    bool firstY = true;
    bool firstZ = true;

    Texture2D meshTexture;
    
    public GameObject MeshBuilderPrefab;

    int PositionFileBaseStreamPos = 0;
    int UVFileBaseStreamPos = 0;
    int NormalFileBaseStreamPos = 0;
    //int positionFileBaseStreamPos = 0;

    int numberOfMeshes = 0;
    int totalNumberOfVertex = 0;
    int totalTriangleCount = 0;
    bool loadingPositionComplete = false;
    // Use this for initialization
    void Start()
    {
        /*
        WWW wwwPosition = new WWW (urlPosition);
        while (!wwwPosition.isDone) {
            posText.text = "POSITION: DOWNLOADING...";
        
        }
        yield return wwwPosition;
        posText.text = "POSITION: DOWNLOADING FINISHED";
        */


        //StartCoroutine(readBinFile(urlPosition, "POSITION"));
        //readBinFile(urlPosition, "POSITION");


        //StartCoroutine(readFile(urlPosition, "POSITION"));


        LoadingCanvas.gameObject.SetActive(false);
        Screen.sleepTimeout = 0;
        positionList = new List<Vector3[]>();
        triangleList = new List<int[]>();
        normalList = new List<Vector3[]>();
        uvList = new List<Vector2[]>();

        currentUrlPosition = "";
        currentUrlNormal = "";
        currentUrlUV = "";
        currentUrlHi = "";
        currentUrlAnnotations = "";

        //currentUrlPosition = urlPosition;
        //currentUrlNormal = urlNormal;
        //currentUrlUV = urlUV;
        //currentUrlHi = urlHi;
        //currentUrlAnnotations = urlAnnotations;
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    //Zoffset = 20;
        //    StartCoroutine(readPositionFileMemoryStream(urlPosition, "POSITION"));
        //    StartCoroutine(readNormalFileMemoryStream(urlNormal, "NORMAL"));
        //    StartCoroutine(readTextureFileMemoryStream(urlHi,"TEXUTRE"));
        //    StartCoroutine(CreateMeshes());

        //    //StartCoroutine(CreateMeshes());
        //}

        //readPositionFileMemoryStream(urlPosition, "POSITION");//BYTE SIZE: 4694832, 4694832/3 = 1564944, 
        //4694832 /12("Each vertex position has 3 components (x, y, z) and therefore consists of 3 x 32 bits = 96 bits = 12 bytes") = 391236

        //readNormalFileMemoryStream(urlNormal, "NORMAL");//BYTE SIZE: 4694832
        //readTextureFileMemoryStream(urlHi, "TEXTURE");
        //readUVFileMemoryStream(urlUV, "UV");

        //StartCoroutine(readFile(urlNormal,  "NORMAL"));
        //StartCoroutine(readFile(urlUV,  "UV"));
        //StartCoroutine(readFile(urlHi,  "HI"));
        //StartCoroutine(readFile(urlAnnotations,  "ANNOTATION"));

    }


    private IEnumerator readUVFileMemoryStream(string urlPath, string fileType)
    {
        System.GC.Collect();
        WWW www = new WWW(urlPath);
        while (!www.isDone)
        {
            //text.text = fileType + " : DOWNLOADING...";
            //www.
            //textBoard.text = fileType + " BINARY FILE LOADING, PROGRESS: " + www.progress;

        }

        int uOrv = 1;
        int fileposition = 1;
        int vertexposition = 0;
        uv = new Vector2[meshTestVertexCount];
        uvList = new List<Vector2[]>();
        Vector2 currentuv = new Vector2();
        using (MemoryStream memStream = new MemoryStream(www.bytes))
        {
            using (BinaryReader br = new BinaryReader(memStream))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    if (fileposition <= meshTestVertexCount * 2)//
                    {

                        float i = br.ReadSingle();

                        if (uOrv == 1)
                        {
                            currentuv.x = i;
                            uOrv++;

                        }
                        else if (uOrv == 2)
                        {
                            currentuv.y = i;
                            uv[vertexposition] = currentuv;

                            vertexposition++;
                            uOrv = 1;

                        }
                        fileposition++;
                    }
                    else
                    {

                        uvList.Add(uv);
                        //totalNumberOfVertex += vertexposition;
                        fileposition = 1;
                        vertexposition = 0;
                        uOrv = 1;
                        //positions = new Vector3[meshTestVertexCount];
                        //triangles = new int[meshTestVertexCount];
                        uv = new Vector2[meshTestVertexCount];
                    }
                }


            }
        }


        uvList.Add(uv);
        Debug.Log("UV GENERATED: "+uvList.Count);
        loadingUVComplete = true;

        yield return 10f;
        //GameObject newMeshBuilder = (GameObject)Instantiate(MeshBuilderPrefab, new Vector3(0,0,0), Quaternion.identity);

        //GameObject.FindGameObjectsWithTag("meshBuilder")[0].GetComponent<MeshBuilder>().buildNewMesh(positions, triangles, vertexColors, normals, meshTexture,uv);
        //newMeshBuilder.GetComponent<MeshBuilder>().buildNewMesh(positions, triangles, vertexColors, normals, meshTexture, uv);

        //GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().initialiseMeshList(1);

        //GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().createNewMeshBuilder(positions, triangles, normals, meshTexture, uv);


    }

    private IEnumerator readTextureFileMemoryStream(string urlPath, string fileType)
    {
        System.GC.Collect();
        WWW www = new WWW(urlPath);
        while (!www.isDone)
        {
            //text.text = fileType + " : DOWNLOADING...";
            //www.
            //textBoard.text = fileType + " BINARY FILE LOADING, PROGRESS: " + www.progress;

        }

        meshTexture = www.texture;
        loadingTextureComplete = true;

        yield return 10f;




        //GameObject.FindGameObjectsWithTag("meshBuilder")[0].GetComponent<MeshBuilder>().buildNewMesh(positions, triangles, vertexColors, normals, meshTexture);

    }


    private IEnumerator readNormalFileMemoryStream(string urlPath, string fileType)
    {
        System.GC.Collect();






        WWW www = new WWW(urlPath);
        while (!www.isDone)
        {
            //text.text = fileType + " : DOWNLOADING...";
            //www.
            //textBoard.text = fileType + " BINARY FILE LOADING, PROGRESS: " + www.progress;





        }

        //textBoard.text = fileType + " BINARY FILE FULLY LOADED, BYTE SIZE: ";
        byte[] datafromwww = www.bytes;
        //textBoard.text = fileType + " BINARY FILE FULLY LOADED, BYTE SIZE: " + datafromwww.Length;//byte length: 4694832

        int fileposition = 1;
        int vertexposition = 0;
        int xORyOrz = 1;
        normalList = new List<Vector3[]>();
        normals = new Vector3[meshTestVertexCount];
        Vector3 currentNormal = new Vector3();
        using (MemoryStream memStream = new MemoryStream(www.bytes))
        {
            using (BinaryReader br = new BinaryReader(memStream))
            {

                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    if (fileposition <= meshTestVertexCount * 3)//
                    {
                        float i = br.ReadSingle();

                        if (xORyOrz == 1)
                        {
                            currentNormal.x = i;
                            xORyOrz++;

                        }
                        else if (xORyOrz == 2)
                        {
                            currentNormal.y = i;
                            xORyOrz++;
                        }
                        else if (xORyOrz == 3)
                        {
                            currentNormal.z = i;
                            normals[vertexposition] = currentNormal;

                            vertexposition++;
                            xORyOrz = 1;

                        }



                        //normals[vertexposition] = i;


                        fileposition++;
                    }
                    else
                    {
                        //positionList.Add(positions);
                        //triangleList.Add(triangles);
                        normalList.Add(normals);
                        //totalNumberOfVertex += vertexposition;
                        fileposition = 1;
                        vertexposition = 0;
                        xORyOrz = 1;
                        //positions = new Vector3[meshTestVertexCount];
                        //triangles = new int[meshTestVertexCount];
                        normals = new Vector3[meshTestVertexCount];
                        //numberOfMeshes++;

                        //Debug.Log("NUMBER OF MESH: " + numberOfMeshes);
                    }
                }
            }
        }

        normalList.Add(normals);
        loadingNormalComplete = true;

        yield return 10f;
    }

    private IEnumerator readPositionFileMemoryStream(string urlPath, string fileType)
    {


        //LoadingCanvas.gameObject.SetActive(true);
        WWW www = new WWW(urlPath);
        while (!www.isDone)
        {
            //text.text = fileType + " : DOWNLOADING...";
            //www.
            //textBoard.text = fileType + " BINARY FILE LOADING, PROGRESS: " + www.progress;

            //TextIndicator.GetComponent<Text>().text = ((int)www.progress * 100).ToString() + "%";
            //TextLoading.gameObject.SetActive(true);

            //LoadingBar.GetComponent<Image>().fillAmount = (int)(www.progress * 100) / 100;


        }
        //TextIndicator.GetComponent<Text>().text = "LOADED";

        //TextLoading.gameObject.SetActive(false);


        //yield return 10f;

        int triangleCount = 0;

        //byte[] datafromwww = www.bytes;
        
        //textBoard.text = fileType + " BINARY FILE FULLY LOADED, BYTE SIZE: " + datafromwww.Length;

        //Debug.Log(fileType + " BINARY FILE FULLY LOADED, BYTE SIZE: " + datafromwww.Length* 4+", NUMBER OF VERTEX: "+ datafromwww.Length * 4/12);

        int Zoffset = 0;

        if (Application.platform == RuntimePlatform.Android)
        {
            Zoffset = 0;

        }
        int fileposition = 1;
        int vertexposition = 0;
        int xORyOrz = 1;
        positions = new Vector3[meshTestVertexCount];
        triangles = new int[meshTestVertexCount];
        vertexColors = new Color[meshTestVertexCount];
        positionList = new List<Vector3[]>();
        triangleList = new List<int[]>();
        Vector3 currentVertex = new Vector3();
        //float nextValue = 0f;
        using (MemoryStream memStream = new MemoryStream(www.bytes))
        {
            using (BinaryReader br = new BinaryReader(memStream))
            {

                
                //while (br.BaseStream.Position != br.BaseStream.Length)
                while (br.BaseStream.Position != br.BaseStream.Length)
                {


                    //4 bytes for each x, y, z

                    if (fileposition <= meshTestVertexCount * 3)//
                    {
                        //byte[] temp = new byte[4];
                        //memStream.Read(temp, 0, 4);

                        //if (BitConverter.IsLittleEndian)
                        //{
                        //    Array.Reverse(temp);
                        //}
                        //float i = BitConverter.ToSingle(temp, 0);


                        float i = br.ReadSingle();//4 bytes, means it reads a x or y or z of a vertex

                        if (xORyOrz == 1)
                        {
                            currentVertex = new Vector3();
                            currentVertex.x = i;
                            xORyOrz++;

                            if (firstX)
                            {
                                minX = i;
                                maxX = i;
                                firstX = false;
                            }
                            else
                            {
                                if (i > maxX)
                                {
                                    maxX = i;
                                }
                                else if (i < minX)
                                {
                                    minX = i;
                                }
                            }
                        }
                        else if (xORyOrz == 2)
                        {
                            currentVertex.y = i;
                            xORyOrz++;

                            if (firstY)
                            {
                                minY = i;
                                maxY = i;
                                firstY = false;
                            }
                            else
                            {
                                if (i > maxY)
                                {
                                    maxY = i;
                                }
                                else if (i < minY)
                                {
                                    minY = i;
                                }
                            }
                        }
                        else if (xORyOrz == 3)
                        {
                            currentVertex.z = i+Zoffset;
                            positions[vertexposition] = currentVertex;
                            triangles[vertexposition] = vertexposition;


                            //vertexColors[vertexposition] = new Color(255, 0, 0, 1);
                            vertexposition++;
                            xORyOrz = 1;

                            if (firstZ)
                            {
                                currentVertex.z = i;
                                currentVertex.z = i;
                                firstZ = false;
                            }
                            else
                            {
                                if (currentVertex.z > maxZ)
                                {
                                    maxZ = currentVertex.z;
                                }
                                else if (currentVertex.z < minZ)
                                {
                                    minZ = currentVertex.z;
                                }
                            }
                        }


                        if (fileposition % 9 == 0)
                        {

                            triangleCount++;
                        }

                        //Debug.Log("READ FLOAT FROM FILE: "+i);

                        fileposition++;
                        

                    }//END OF THE CURRENT MESH
                    else
                    {
                        positionList.Add(positions);
                        triangleList.Add(triangles);
                        totalNumberOfVertex += vertexposition;
                        fileposition = 1;
                        vertexposition = 0;
                        xORyOrz = 1;
                        positions = new Vector3[meshTestVertexCount];
                        triangles = new int[meshTestVertexCount];
                        numberOfMeshes++;
                        
                        Debug.Log("NUMBER OF MESH: " + numberOfMeshes);
                    }
                    //
                }//END OF THE WHOLE FILE
            }

        }
        
        positionList.Add(positions);
        triangleList.Add(triangles);
        numberOfMeshes++;
        totalNumberOfVertex += vertexposition;
        loadingPositionComplete = true;
        yield return 10f;
        //textBoard.text = fileType + " LOADING COMPLETE!";


        centerX = (maxX - minX) / 2 + minX;
        centerY = (maxY - minY) / 2 + minY;
        centerZ = (maxZ - minZ) / 2 + minZ;


        Debug.Log("MESH INFO PROCESSED, TRIANGLE COUNT: "+triangleCount+", VERTEX COUNT: "+totalNumberOfVertex+", FILE POS: "+fileposition);

        Debug.Log("MAX X: "+  maxX + ", MIN X: " + minX);

        Debug.Log("MAX Y: " + maxY + ", MIN Y: " + minY);
        Debug.Log("MAX Z: " + maxZ + ", MIN Z: " + minZ);
        Debug.Log("CENTER: "+centerX+", "+centerY+", "+centerZ );
        if (Application.platform != RuntimePlatform.Android)
        {
            Camera.main.transform.position = new Vector3(centerX, centerY, maxZ + 40);//maxZ+40
            Camera.main.transform.LookAt(new Vector3(centerX, centerY, centerZ));
        }
        Debug.Log("CAMERA POSITION: X: "+ Camera.main.transform.position.x+", Y: "+ Camera.main.transform.position.y+", Z: "+ Camera.main.transform.position.z);


        //find
        //Debug.Log("MESH BUILT");


        System.GC.Collect();
        Debug.Log("GC CALLED");


    }
    bool loadingNormalComplete = false;
    bool creatingMeshComplete = false;
    bool loadingUVComplete = false;
    bool loadingTextureComplete = false;
    bool loadingModelComplete = false;

    private IEnumerator CreateMeshes()
    {
        GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().initialiseMeshList(numberOfMeshes);


        for (int i = 0; i < numberOfMeshes; i++)
        {
            //Debug.Log("GENERATING: "+(numberOfMeshes+1)+"th mesh");
            Vector3[] tempPos = positionList[i];
            int[] tempTriangle = triangleList[i];
            Vector3[] tempNormal = normalList[i];
            Vector2[] tempUV = uvList[i];
            GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().createNewMeshBuilder(tempPos, tempTriangle, tempNormal, this.meshTexture, tempUV);
            yield return 20;
        }
        numberOfMeshes = 0;

        if (Application.platform == RuntimePlatform.Android)
        {
            //textBoard.text = "PLATFORM IS ANDORID!";

        }
        loadingModelComplete = true;
        if (Application.platform != RuntimePlatform.Android)
        {
            Camera.main.transform.RotateAround(new Vector3(centerX, centerY, centerZ), new Vector3(0, 1, 0), -60f);
        }
        //creatingMeshComplete = true;
        //yield return 10;
    }


    private void LoadAndSaveFile()
    {


    }


    private IEnumerator readFile(string urlPath, string fileType)
    {
        WWW www = new WWW(urlPath);
        while (!www.isDone)
        {
            //text.text = fileType + " : DOWNLOADING...";
            //www.
            //textBoard.text = fileType + " BINARY FILE LOADING";
            //www.
        }
        //www.
        yield return www;
        //www.by
        string returnedText = www.text;

        //textBoard.text = fileType + " BINARY FILE LOADED, SIZE: " + returnedText.Length;

        //string[] lines = returnedText.Split("\n"[0]);



        //if (File.Exists(positionFileName))


        //{



        //    textBoard.text = fileType + " BINARY FILE ALREADY EXIST";

        //    yield return null;


        //}
        string filePath = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Application.persistentDataPath + "/" + positionFileName;
        }
        else// if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            filePath = positionFileName;
        }
        StreamWriter sr = File.CreateText(filePath);

        //textBoard.text = fileType + " AFTER FILE CREATED AND BEFORE FILE WRITTEN";

        sr.WriteLine(returnedText);

        //textBoard.text = fileType + " BINARY FILE SAVED";

        sr.Close();


        yield return 1f;
        //textBoard.text = fileType + " BINARY FILE SAVED";
        //<----------------READ BINARY FILE------------------>

        int length = 0;

        int pos = 0;
        // 1.
        //65,534 is the vertex limit for a mesh
        using (BinaryReader b = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None)))
        {
            // 2.
            // Position and length variables.
            
            // 2A.
            // Use BaseStream.
            length = (int)b.BaseStream.Length;


            //while (b.BaseStream.Position != b.BaseStream.Length)
            while (b.BaseStream.Position != 20)

            //while (pos < length)
            {
                // 3.
                // Read integer.
                //long v = b.ReadInt16();//Int32: error , Int64: type is long instead of int + error
                //if(b.PeekChar() == -1)
                //{
                //    break;
                //}
                //b.
                //int v2 = b.ReadInt16();

                int v3 = b.Read();
                //Console.WriteLine(v);
                //textBoard.text = v3.ToString();
                if (Application.platform != RuntimePlatform.Android)
                {
                    Debug.Log(v3.ToString());
                }
                // textBoard.text = v2.ToString();

                // 4.
                // Advance our position variable.
                //pos += sizeof(int);

                pos++;
            }
        }

        //textBoard.text = fileType + " BINARY FILE READ, \n LENGTH OF FILE: "+length+"\n READ LOOP NUMBER: "+pos;
        //LENGTH OF FILE: 9038538, LOOP NUMBER TO READ INT16: 4519269 = 9038538/2

        //text.text = fileType + " : DOWNLOADING FINISHED, LINE NUMBER: " + lines.Length;

        //if (fileType == "POSITION")
        //{
        //    positionFirstLineText.text += "\n" + lines[0];
        //}

    }

    private void readBinFile(string urlPath, string fileType)
    {


        int length = 0;


        // 1.
        using (BinaryReader b = new BinaryReader(File.Open(urlPath, FileMode.Open, FileAccess.Read, FileShare.None)))
        {
            // 2.
            // Position and length variables.
            int pos = 0;
            // 2A.
            // Use BaseStream.
            length = (int)b.BaseStream.Length;

            //while (pos < length)
            //{
            //    // 3.
            //    // Read integer.
            //    int v = b.ReadInt32();
            //    Console.WriteLine(v);

            //    // 4.
            //    // Advance our position variable.
            //    pos += sizeof(int);
            //}
        }
        //yield return length;

        //textBoard.text = fileType + " BINARY FILE READ";

    }

    // Update is called once per frame
    void Update()
    {

        loadModel();


    }

    void clearText()
    {
        //textBoard.text = "";
    }

    void clearGarbageAndUnusedResources()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    public void setCurrentModelURLS(string pos, string nor, string uv, string hi, string anno)
    {

        //        private string currentUrlPosition;
        //private string currentUrlNormal;
        //private string currentUrlUV;
        //private string currentUrlHi;
        //private string currentUrlAnnotations;

        loadingPositionComplete = false;
        loadingNormalComplete = false;
        loadingUVComplete = false;
        loadingTextureComplete = false;



        this.currentUrlPosition = pos;
        this.currentUrlNormal = nor;
        this.currentUrlUV = uv;
        this.currentUrlHi = hi;
        this.currentUrlAnnotations = anno;



        this.loadingModelComplete = false;
    }



    public void setLoadingModelComplete(bool complete)
    {
        this.loadingModelComplete = complete;
    }

    public bool getLoadingModelComplete()
    {
        return loadingModelComplete;
    }

    void loadModel()
    {
        //textBoard.text = "CAMERA X: " + Camera.main.transform.position.x + ", Y: " + Camera.main.transform.position.y + ", Z: " + Camera.main.transform.position.z;

        if ((Application.platform != RuntimePlatform.Android))//ON COMPUTER
        {
           //Camera.main.transform.RotateAround(new Vector3(centerX, centerY, centerZ), new Vector3(0, 1, 0), 0.5f);

            //if (Input.GetKeyDown(KeyCode.A) && !loadingPositionComplete && currentUrlPosition!="")
           if ( !loadingPositionComplete && currentUrlPosition != "")

           {

                Debug.Log("CREATE MESHES");
                //destroyMeshBuilders();
                StartCoroutine(readPositionFileMemoryStream(currentUrlPosition, "POSITION"));
                //StartCoroutine(CreateMeshes());

            }



            if (loadingPositionComplete && !loadingNormalComplete && currentUrlNormal!="")
            {
                StartCoroutine(readNormalFileMemoryStream(currentUrlNormal, "NORMAL"));

            }

            if (loadingNormalComplete && !loadingTextureComplete && currentUrlHi!="")
            {
                StartCoroutine(readTextureFileMemoryStream(currentUrlHi, "TEXUTRE"));

            }
            if (loadingTextureComplete && !loadingUVComplete && currentUrlUV!="")
            {
                StartCoroutine(readUVFileMemoryStream(currentUrlUV, "UV"));
                StartCoroutine(CreateMeshes());

            }

        }
        else//ON ANDROID
        {
            if (!loadingPositionComplete && currentUrlPosition!="")
            {
                StartCoroutine(readPositionFileMemoryStream(currentUrlPosition, "POSITION"));
            }
            if (loadingPositionComplete && !loadingNormalComplete && currentUrlNormal!="")
            {
                StartCoroutine(readNormalFileMemoryStream(currentUrlNormal, "NORMAL"));

            }

            if (loadingNormalComplete && !loadingTextureComplete && currentUrlHi!="")
            {
                StartCoroutine(readTextureFileMemoryStream(currentUrlHi, "TEXUTRE"));

            }
            if (loadingTextureComplete && !loadingUVComplete && currentUrlUV!="")
            {
                StartCoroutine(readUVFileMemoryStream(currentUrlUV, "UV"));
                StartCoroutine(CreateMeshes());

            }
        }
    }
}

