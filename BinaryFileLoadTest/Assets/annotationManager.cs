using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class annotationManager : MonoBehaviour {


    private string currentAnnoURL;
    private bool currentAnnoParsed = false;
    public Button nextAnnoButton;
    // Use this for initialization
    void Start() {
        currentAnnoURL = "";
        initialParsedStrings = new List<string>();
        initialParsedStringMarks = new List<int>();

        facePointsPositions = new List<int>();
        faceTriangles = new List<Vector3[]>();
        linePoints = new List<Vector3>();
    }

    public void clearCurrentAnno()
    {


        currentAnnoURL = "";
        initialParsedStrings = new List<string>();
        initialParsedStringMarks = new List<int>();

        facePointsPositions = new List<int>();
        faceTriangles = new List<Vector3[]>();


        countOfAnnotations = 0;
        //currentAnnoURL = "";
        initialParsedStringMarks.Clear();
        initialParsedStringMarks = new List<int>();
        annoFileToBeParsed = "";
        hasAnno = false;
        annotationBuilt = false;
        loadAnnoFileFinished = false;
        currentAnnoBeginPos = 0;
        currentlyAtMark = 0;
        currentMarkerPosition = 0;
        linePointNum = 0;
        triangleNum = 0;
    }

    int count = 0;
    // Update is called once per frame
    void Update() {
        if (currentAnnoURL != "" && !loadAnnoFileFinished)
        {
            StartCoroutine(loadAnnoFile());
        }

        if (loadAnnoFileFinished && hasAnno)
        {
            //Debug.Log(splitAnnoFile[count]);
            //if (count + 1 < splitAnnoFile.Length)

            //{
            //    count++;
            //}
            if (annoFileToBeParsed.Contains(strParsed))
            {
                StartCoroutine(parseInitialAnnoFile());
            }
            //else
            //{
            //    if (!finalReached)
            //    {
            //        currentMarkerPosition += (annoFileToBeParsed.Length + strParsed.Length);
            //        finalReached = true;
            //        Debug.Log("MAKER REACHED TO:" + currentMarkerPosition.ToString());
            //    }
            //}
        }

        if (gotCurrentAnnoContent)
        {
            if (currentAnnoType == "FACE")
            {
                if (facesString.Contains("c"))
                {
                    StartCoroutine(parseFace());
                }
                else
                {
                    if (!annotationBuilt)
                    {
                        StartCoroutine(buildFaceAnnotation());
                    }
                }
            }
            else if(currentAnnoType == "LINE")
            {
                if (lineString.Contains("z"))
                {
                    StartCoroutine(parseLine());
                }
                else
                {
                    if(!annotationBuilt)
                    {
                        StartCoroutine(buildLineAnnotation());
                    }
                }

            }
        }
    }

    public GameObject lineRenderer;
    IEnumerator buildLineAnnotation()
    {
        if(lineRenderer)
        {

            LineRenderer lr =
            lineRenderer.gameObject.GetComponent<LineRenderer>();

            if (Application.platform == RuntimePlatform.Android)
            {
                lr.startWidth = 0.5f * 0.025f;
                lr.endWidth = 0.5f * 0.025f;
            }
            else
            {
                lr.startWidth = 0.5f;
                lr.endWidth = 0.5f;
            }
            lr.startColor = new Color(0,255,0,255);
            lr.endColor = new Color(0, 255, 0, 255);
            lr.useWorldSpace = false;
            lr.positionCount = linePoints.Count;
            for(int i=0;i<linePoints.Count;i++)
            {
                Vector3 temp = new Vector3();
                if (Application.platform == RuntimePlatform.Android)
                {
                    //temp = Vector3.Scale(linePoints[i], new Vector3(0.025f, 0.025f, 0.025f));
                    temp = linePoints[i];
                }
                else
                {
                    //temp = Vector3.Scale(linePoints[i], new Vector3(0.025f, 0.025f, 0.025f));

                    temp = linePoints[i];
                }
                //lr.SetPosition(i, linePoints[i]);
                lr.SetPosition(i, temp);

            }

            if (Application.platform == RuntimePlatform.Android)
            {
                lineRenderer.gameObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
                //lineRenderer.gameObject.GetComponent<LineRenderer>().transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
                //lineRenderer.gameObject.GetComponent<LineRenderer>().transform.localScale.Set(0.025f, 0.025f, 0.025f);
                //lineRenderer.gameObject.transform.localScale.Set(0.025f, 0.025f, 0.025f);
            }

        }




        annotationBuilt = true;
        if (annotationProcessStatusField)
        {
            annotationProcessStatusField.text = "LINE BUILT";
        }

        if (BugInfoCanvas)
        {
            if (!BugInfoCanvas.gameObject.activeSelf)
            {
                BugInfoCanvas.gameObject.SetActive(true);
            }
            if (propertyField)
            {
                propertyField.text = currentAnnoProperty;
            }
            if (markerPosField)
            {
                markerPosField.text = "x: " + currentAnnoMarkerPos.x + ", y: " + currentAnnoMarkerPos.y + ", z:" + currentAnnoMarkerPos.z;
            }


            
        }

        if (nextAnnoButton)
        {
            if (!nextAnnoButton.gameObject.activeSelf)
            {
                nextAnnoButton.gameObject.SetActive(true);
            }
        }
        yield return 1f;
    }


    public GameObject MeshBuilderPrefab;
    public Material redMat;
    bool annotationBuilt = false;
    public InputField annotationProcessStatusField;
    public Canvas BugInfoCanvas;
    public InputField propertyField;
    public InputField markerPosField;
    IEnumerator buildFaceAnnotation()
    {
        facePointsPositions = new List<int>();
        int numberOfPoints = faceTriangles.Count * 3;
        Debug.Log("NUM OF POINTS(BEFORE PROCESSING): "+numberOfPoints);
        for(int i=0; i< numberOfPoints;i++)
        {
            facePointsPositions.Add(i);
        }

        Vector3[] tempPos = new Vector3[numberOfPoints];
        Vector4[] facePointsPassedToShader = new Vector4[numberOfPoints];
        int pointAdded = 0;
        for(int i = 0; i < faceTriangles.Count; i++) 
        {

            Vector3[] temp = faceTriangles[i];
            tempPos[pointAdded] = temp[0];
            facePointsPassedToShader[pointAdded] = temp[0];
            pointAdded++;
            //facePointsPassedToShader.
            tempPos[pointAdded] = temp[1];
            facePointsPassedToShader[pointAdded] = temp[1];
            pointAdded++;

            tempPos[pointAdded] = temp[2];
            facePointsPassedToShader[pointAdded] = temp[2];
            pointAdded++;
        }

        List<Vector4> facePointsPassedToShaderFinalList = new List<Vector4>();
        //facePointsPassedToShaderFinal.cop
        //int noRepeatListLengtt = 0;
        for (int i=0;i<facePointsPassedToShader.Length;i++)
        {
            bool currentValueHasRepeat = false;
            for(int a = 0; a < facePointsPassedToShader.Length && a != i; a++)
            {

                if (
                   facePointsPassedToShader[a].x == facePointsPassedToShader[i].x &&
                   facePointsPassedToShader[a].y == facePointsPassedToShader[i].y &&
                   facePointsPassedToShader[a].z == facePointsPassedToShader[i].z
                    )
                {
                    //facePointsPassedToShader[i] = new Vector4(-999,-999,-999,-999);
                    currentValueHasRepeat = true;
                    break;
                }
            }

            if(!currentValueHasRepeat)
            {
                facePointsPassedToShaderFinalList.Add(facePointsPassedToShader[i]);
            }
        }

        //facePointsPassedToShader = new Vector4[facePointsPassedToShaderFinalList.Count];
        facePointsPassedToShader = new Vector4[1000];

        facePointsPassedToShaderFinalList.CopyTo(facePointsPassedToShader);
        Debug.Log("LENGTH OF PASSED FACE POINT NUMBER(AFTER DEL REPEATS): "+ facePointsPassedToShaderFinalList.Count);
        GameObject.FindGameObjectsWithTag("shaderUpdate")[0].GetComponent<shaderVariableUpdate>().setLoopNumber(facePointsPassedToShaderFinalList.Count);

        GameObject.FindGameObjectsWithTag("shaderUpdate")[0].GetComponent<shaderVariableUpdate>().setAnnotationFaces(facePointsPassedToShader, 1000);


        //<----------------- TRIANGLE LIST TO BUILD THE MESH------------------>
        //int[] tempTriangle = new int[facePointsPositions.Count];
        //for (int i = 0; i < tempTriangle.Length; i++)
        //{
        //    tempTriangle[i] = facePointsPositions[i];
        //}


        //GameObject newMeshBuilder = (GameObject)Instantiate(MeshBuilderPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //newMeshBuilder.GetComponent<MeshBuilder>().buildNewMesh(tempPos, tempTriangle, null, null, null);

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    newMeshBuilder.gameObject.transform.localScale = new Vector3(1.001f * 0.025f, 1.001f * 0.025f, 1.001f * 0.025f);
        //}
        //else
        //{
        //    newMeshBuilder.gameObject.transform.localScale =
        //        new Vector3(1.001f, 1.001f, 1.001f);

        //    //newMeshBuilder.gameObject.transform.localScale = new Vector3(1.001f * 0.025f, 1.001f * 0.025f, 1.001f * 0.025f);


        //}
        //newMeshBuilder.gameObject.GetComponent<MeshRenderer>().material = redMat;
        //<----------------- TRIANGLE LIST TO BUILD THE MESH------------------>

        annotationBuilt = true;
        if(annotationProcessStatusField)
        {
            annotationProcessStatusField.text = "PASSED ANNO";
        }

        if(BugInfoCanvas)
        {
            if(!BugInfoCanvas.gameObject.activeSelf)
            {
                BugInfoCanvas.gameObject.SetActive(true);
                if(propertyField)
                {
                    propertyField.text = currentAnnoProperty;
                }
                if(markerPosField)
                {
                    markerPosField.text = "x: "+currentAnnoMarkerPos.x+", y: "+ currentAnnoMarkerPos.y+", z:"+ currentAnnoMarkerPos.z;
                }


            }
        }

        if (nextAnnoButton)
        {
            if (!nextAnnoButton.gameObject.activeSelf)
            {
                nextAnnoButton.gameObject.SetActive(true);
            }
        }
        yield return 1f;
    }

    int triangleNum = 0;
    IEnumerator parseFace()
    {
        int tempIndex = facesString.IndexOf("}}");
        string oneTriangleString = facesString.Substring(facesString.IndexOf(":{") + 2, facesString.IndexOf("}}") - (facesString.IndexOf(":{") + 2));
        bool xFirst = false;
        
        if(oneTriangleString.IndexOf("x") < oneTriangleString.IndexOf("z"))
        {
            xFirst = true;
        }
        else
        {
            xFirst = false;
        }

        triangleNum++;
        //Debug.Log("ONE TRIANGLE STRING: ("+triangleNum+"): "+oneTriangleString);

        Vector3[] currentTriangle = new Vector3[3];
        string[] trianglePoints = oneTriangleString.Split("}"[0]);
        //faceTriangles = new List<Vector3[]>();
        //Debug.Log("ONE TRIANGLE POINT NUMBER: "+trianglePoints.Length);
        for(int i=0;i<trianglePoints.Length;i++)
        {
            //each element contains x,y,z 

            if(trianglePoints[i].Contains(":{"))
            {
                trianglePoints[i] =
                trianglePoints[i].Substring
                (trianglePoints[i].IndexOf(":{")+2,
                trianglePoints[i].Length - (trianglePoints[i].IndexOf(":{") + 2));
            }
            string xyz = trianglePoints[i];


            string[] pointCoordinations = xyz.Split(","[0]);
            for(int a=0;a<pointCoordinations.Length;a++)
            {
                pointCoordinations[a] = pointCoordinations[a].Substring
                (pointCoordinations[a].IndexOf(":")+1, pointCoordinations[a].Length-(pointCoordinations[a].IndexOf(":") + 1));


                //Debug.Log("TRIANGLE("+triangleNum+")"+" POINT("+i+")"+"COOR("+a+"):"+ pointCoordinations[a]);

            }
            
            float x = float.Parse(pointCoordinations[0]);
            float y = float.Parse(pointCoordinations[1]);
            float z = float.Parse(pointCoordinations[2]);

            if (xFirst)
            {
                currentTriangle[i] = new Vector3(x, y, z);
            }
            else
            {
                currentTriangle[i] = new Vector3(z, y, x);
            }

            if(i == 2)
            {
                faceTriangles.Add(currentTriangle);
                currentTriangle = new Vector3[3];
            }

            //Vector3 currentFace = new Vector3(x,y,z);
            //facePositions.Add(currentFace);

            //Debug.Log("x:"+x+"y:"+y+"z:"+z);
            //Debug.Log("TRIANGLE("+triangleNum+")"+" POINT("+i+"): "+trianglePoints[i]);
        }

        //Debug.Log("FACE NUMBER AFTER PARSING: "+faceTriangles.Count+", TRIANGLE NUM: "+triangleNum);

        facesString = facesString.Substring(facesString.IndexOf("}}")+2, facesString.Length - (facesString.IndexOf("}}") + 2));
        yield return 1f;
    }

    int linePointNum = 0;
    IEnumerator parseLine()
    {
        int tempIndex = lineString.IndexOf("}");
        string onePointString = lineString.Substring(lineString.IndexOf("{") + 1, lineString.IndexOf("}") - (lineString.IndexOf("{") + 1));
        Debug.Log("ONE POINT STRING: "+onePointString);
        lineString = lineString.Substring(lineString.IndexOf("}") + 1, lineString.Length - (lineString.IndexOf("}") + 1));
        //Debug.Log("ONE POINT STRING ONELESS: " + lineString);
        linePointNum++;

        int indexOfX = onePointString.IndexOf("x");
        int indexOfY = onePointString.IndexOf("y");
        int indexOfZ = onePointString.IndexOf("z");

        int min = 0;
        int max = 0;
        string maxIndex = "";
        string minIndex = "";
        max = indexOfX > indexOfY ? indexOfX : indexOfY;
        //maxIndex = indexOfX > indexOfY ? "x" : "y";
        max = indexOfZ > max ? indexOfZ : max;
        //maxIndex = indexOfZ > max ? "z" : maxIndex;

        min = indexOfX < indexOfY ? indexOfX : indexOfY;
        //minIndex = indexOfX < indexOfY ? "x" : "y";
        min = indexOfZ < min ? indexOfZ : min;
        //minIndex = indexOfZ < min ? "z" : minIndex;
        if(max == indexOfX)
        {
            maxIndex = "x";
        }
        else if(max == indexOfY)
        {
            maxIndex = "y";
        }
        else if(max == indexOfZ)
        {
            maxIndex = "z";
        }

        if (min == indexOfX)
        {
            minIndex = "x";

        }
        else if (min == indexOfY)
        {
            minIndex = "y";
        }
        else if (min == indexOfZ)
        {
            minIndex = "z";
        }
        //Debug.Log("POINT COOR: MAX: " + maxIndex + ", MIN: " + minIndex);
        //<-----------GET POINTS------------>
        Vector3 currentPoint = new Vector3();
        string[] linePointCoordinations = onePointString.Split(","[0]);
        for (int i = 0; i < linePointCoordinations.Length; i++)
        {

            string temp = linePointCoordinations[i].Substring(linePointCoordinations[i].IndexOf(":")+1, linePointCoordinations[i].Length - (linePointCoordinations[i].IndexOf(":") + 1));
            float coor = float.Parse(temp);
            //if (linePointCoordinations[i].IndexOf(":") -2 == max)
            //{
            if (i == 0)
            {
                if (minIndex == "x")
                {
                    currentPoint.x = coor;
                }
                else if (minIndex == "z")
                {
                    currentPoint.z = coor;
                }
            }
            else if(i == 1)
            {
                currentPoint.y = coor;
            }
            else
            {
                if (maxIndex == "x")
                {
                    currentPoint.x = coor;
                }
                else if (maxIndex == "z")
                {
                    currentPoint.z = coor;
                }
            }

            //}
            //Debug.Log("CURRENT POINT: COOR: "+ temp);
        }

        linePoints.Add(currentPoint);
        Debug.Log("CURRENTPOINT: X: "+currentPoint.x+", Y: "+currentPoint.y+", Z: "+currentPoint.z);

        yield return 1f;
    }

    int currentMarkerPosition = 0;
    bool finalReached = false;
    int tempMinus = 0;
    IEnumerator parseInitialAnnoFile()
    {
        //string temp = annoFileToBeParsed.Substring(0, annoFileToBeParsed.IndexOf(strParsed));
        //initialParsedStrings.Add(temp);
        
        int tempIndex = annoFileToBeParsed.IndexOf(strParsed);
        currentMarkerPosition += (tempIndex + strParsed.Length);
        //tempMinus--;
        initialParsedStringMarks.Add(currentMarkerPosition-strParsed.Length);
        annoFileToBeParsed = annoFileToBeParsed.Substring(annoFileToBeParsed.IndexOf(strParsed) + strParsed.Length, annoFileToBeParsed.Length - 1 - (annoFileToBeParsed.IndexOf(strParsed) + strParsed.Length));
        countOfAnnotations++;

        //Debug.Log("EXTRACTED STRING: ("+ countOfAnnotations+ "): "+temp);
        //Debug.Log("MARKER GIVES STRING: ("+ countOfAnnotations +"): "+ annoFileOrigin.Substring(initialParsedStringMarks[countOfAnnotations-1], strParsed.Length));
        //Debug.Log("THE NUM:" + (countOfAnnotations) + " DATA MARKER POSITION:"+ currentMarkerPosition);
        
        yield return 1f;
    }

    string facesString = "";
    string lineString = "";
    bool currentAnnoSectionParsed = false;
    bool gotCurrentAnnoContent = false;
    List<Vector3[]> faceTriangles;
    List<int> facePointsPositions;
    List<Vector3> linePoints;
    int currentAnnoBeginPos = 0;
    int currentlyAtMark = 0;
    string currentAnnoType = "";
    string currentAnnoProperty = "";
    Vector3 currentAnnoMarkerPos = new Vector3();
    string currentMarkerPos = "";
    public void showAnnotation()
    {
        string temp = "";



        if (currentlyAtMark != -1)
        {
            Debug.Log("ANNO: CURRENT BEGIN POSITION: " + currentAnnoBeginPos + ", END POSITION: " + initialParsedStringMarks[currentlyAtMark]);

            temp = annoFileOrigin.Substring(currentAnnoBeginPos, initialParsedStringMarks[currentlyAtMark] - currentAnnoBeginPos);
        }
        else
        {
            Debug.Log("ANNO: CURRENT BEGIN POSITION: " + currentAnnoBeginPos + ", END POSITION: " + (annoFileOrigin.Length-1));

            temp = 
            annoFileOrigin.Substring
            (
                currentAnnoBeginPos, 
                annoFileOrigin.Length - 1 - currentAnnoBeginPos
            );

        }
        //Debug.Log("ANNO: TEMP IS: "+temp);
        if (currentlyAtMark != -1)
        {
            currentAnnoBeginPos = initialParsedStringMarks[currentlyAtMark];

            currentlyAtMark++;
            if (currentlyAtMark == initialParsedStringMarks.Count)
            {
                //currentAnnoBeginPos = 0;
                currentlyAtMark = -1;
            }
        }
        else
        {
            currentAnnoBeginPos = 0;
            currentlyAtMark = 0;

        }

        if (temp.Contains("faces"))
        {
            if(nextAnnoButton)
            {
                if(nextAnnoButton.gameObject.activeSelf)
                {
                    nextAnnoButton.gameObject.SetActive(false);
                }
            }
            Debug.Log("ANNO: A HIGHLIGHT ANNO");
            facesString = temp.Substring(temp.IndexOf("faces")+8, temp.IndexOf("]") - (temp.IndexOf("faces") + 8));
            Debug.Log("ANNO: FACES STRING: " + facesString);


            currentAnnoType = "FACE";
            gotCurrentAnnoContent = true;
            annotationBuilt = false;
            faceTriangles = new List<Vector3[]>();
            //faceTriangles.Capacity = 1000;
            Vector4[] tempVec4 = new Vector4[0];
            //GameObject.FindGameObjectsWithTag("shaderUpdate")[0].GetComponent<shaderVariableUpdate>().setAnnotationFaces(tempVec4, faceTriangles.Capacity);
            if(lineRenderer)
            {
                lineRenderer.gameObject.GetComponent<LineRenderer>().positionCount = 0;
            }

            if(temp.Contains("properties"))
            {
                string tempPropertyString = temp.Substring(temp.IndexOf("properties")+12, temp.Length  - (temp.IndexOf("properties") + 12));
                string propertystring = tempPropertyString.Substring(0, tempPropertyString.IndexOf("}"));
                Debug.Log("PROPERTY STRING(BEFORE PARSED)" + propertystring);

                currentAnnoProperty = propertystring.Substring(propertystring.IndexOf("{")+1, propertystring.Length  - (propertystring.IndexOf("{") + 1));
                Debug.Log("PROPERTY STRING(AFTER PARSED)" + currentAnnoProperty);
            }
            if(temp.Contains("markerPosition"))
            {
                string tempMarkerposString = temp.Substring(temp.IndexOf("markerPosition") + 16, temp.Length - (temp.IndexOf("properties") + 16));
                string markerPosString = tempMarkerposString.Substring(0, tempMarkerposString.IndexOf("}"));
                Debug.Log("MARKERPOS STRING(BEFORE PARSED)" + markerPosString);

                string[] markerPositionsBeforeParse = markerPosString.Split(","[0]); 
                 
                for(int i=0;i<markerPositionsBeforeParse.Length;i++)
                {
                    //Debug.Log("MARKERPOS(RAW): "+markerPositionsBeforeParse[i]);
                    markerPositionsBeforeParse[i] = markerPositionsBeforeParse[i].Substring
                                             (markerPositionsBeforeParse[i].IndexOf(":")+1,
                                             markerPositionsBeforeParse[i].Length - (markerPositionsBeforeParse[i].IndexOf(":") + 1));
                    Debug.Log("MARKERPOS(PARSED): " + markerPositionsBeforeParse[i]);
                    float currentPos = float.Parse(markerPositionsBeforeParse[i]);
                    if (i == 0)
                    {
                        currentAnnoMarkerPos.x = currentPos;
                    }
                    else if (i == 1)
                    {
                        currentAnnoMarkerPos.y = currentPos;
                    }
                    else
                    {
                        currentAnnoMarkerPos.z = currentPos;
                    }
                }
            }


        }
        else if(temp.Contains("Line"))
        {
            if (nextAnnoButton)
            {
                if (nextAnnoButton.gameObject.activeSelf)
                {
                    nextAnnoButton.gameObject.SetActive(false);
                }
            }
            lineString = 
            temp.Substring(temp.IndexOf("vertices") + 11, temp.IndexOf("]") - (temp.IndexOf("vertices") + 11));
            Debug.Log("ANNO: LINE STRING: " + lineString);
            currentAnnoType = "LINE";
            annotationBuilt = false;
            gotCurrentAnnoContent = true;

            linePoints = new List<Vector3>();



            Vector4[] tempVec4 = new Vector4[0];

            GameObject.FindGameObjectsWithTag("shaderUpdate")[0].GetComponent<shaderVariableUpdate>().setAnnotationFaces(tempVec4, 0);
            //<-------------GET PROPERTIES/MARK POSITION etc-------------------->
            if (temp.Contains("properties"))
            {
                string tempPropertyString = temp.Substring(temp.IndexOf("properties") + 12, temp.Length - (temp.IndexOf("properties") + 12));
                string propertystring = tempPropertyString.Substring(0, tempPropertyString.IndexOf("}"));
                Debug.Log("PROPERTY STRING(BEFORE PARSED)" + propertystring);

                currentAnnoProperty = propertystring.Substring(propertystring.IndexOf("{") + 1, propertystring.Length - (propertystring.IndexOf("{") + 1));
                Debug.Log("PROPERTY STRING(AFTER PARSED)" + currentAnnoProperty);
            }
            if (temp.Contains("markerPosition"))
            {
                string tempMarkerposString = temp.Substring(temp.IndexOf("markerPosition") + 16, temp.Length - (temp.IndexOf("properties") + 16));
                string markerPosString = tempMarkerposString.Substring(0, tempMarkerposString.IndexOf("}"));
                Debug.Log("MARKERPOS STRING(BEFORE PARSED)" + markerPosString);

                string[] markerPositionsBeforeParse = markerPosString.Split(","[0]);

                for (int i = 0; i < markerPositionsBeforeParse.Length; i++)
                {
                    //Debug.Log("MARKERPOS(RAW): "+markerPositionsBeforeParse[i]);
                    markerPositionsBeforeParse[i] = markerPositionsBeforeParse[i].Substring
                                             (markerPositionsBeforeParse[i].IndexOf(":") + 1,
                                             markerPositionsBeforeParse[i].Length - (markerPositionsBeforeParse[i].IndexOf(":") + 1));
                    Debug.Log("MARKERPOS(PARSED): " + markerPositionsBeforeParse[i]);
                    float currentPos = float.Parse(markerPositionsBeforeParse[i]);
                    if (i == 0)
                    {
                        currentAnnoMarkerPos.x = currentPos;
                    }
                    else if (i == 1)
                    {
                        currentAnnoMarkerPos.y = currentPos;
                    }
                    else
                    {
                        currentAnnoMarkerPos.z = currentPos;
                    }
                }
            }
            //<-------------GET PROPERTIES/MARK POSITION etc-------------------->


        }
        else
        {
            if (nextAnnoButton)
            {
                if (nextAnnoButton.gameObject.activeSelf)
                {
                    nextAnnoButton.gameObject.SetActive(false);
                }
            }
            gotCurrentAnnoContent = false;

            if(temp.Contains("BodyPart"))
            {
                Debug.Log("ANNO: A BODYPART ANNO");
                if (annotationProcessStatusField)
                {
                    annotationProcessStatusField.text = "bodypart";
                }
            }
            else
            {
                Debug.Log("ANNO: A IMAGE OR OTHER ANNO(NOT PARSABLE)");
                if (annotationProcessStatusField)
                {
                    annotationProcessStatusField.text = "image/other";
                }

            }
            if (lineRenderer)
            {
                lineRenderer.gameObject.GetComponent<LineRenderer>().positionCount = 0;
            }
            annotationBuilt = false;
            gotCurrentAnnoContent = false;
            //faceTriangles.Capacity = 0;
            Vector4[] tempVec4 = new Vector4[0];

            GameObject.FindGameObjectsWithTag("shaderUpdate")[0].GetComponent<shaderVariableUpdate>().setAnnotationFaces(tempVec4, 0);

            if (nextAnnoButton)
            {
                if (!nextAnnoButton.gameObject.activeSelf)
                {
                    nextAnnoButton.gameObject.SetActive(true);
                }
            }

        }
        //Debug.Log("ANNO FIRST PART: "+temp);
    }


    public void startParsingAnnotationFile(string annoURL)
    {
        currentAnnoURL = annoURL;
    }

    public void setLoadAnnoFileFinished(bool value)
    {
        loadAnnoFileFinished = value;
    }

    bool AnnoFileParsedCompleted = false;
    bool loadAnnoFileFinished = false;
    string[] splitAnnoFile;
    string annoFileToBeParsed;
    string annoFileOrigin;
    int countOfAnnotations = 0;
    int initialStringLength;
    List<string> initialParsedStrings;
    List<int> initialParsedStringMarks;

    string strParsed;

    bool hasAnno = false;

    IEnumerator loadAnnoFile()
    {
        if (currentAnnoURL != "")
        {
            WWW www = new WWW(currentAnnoURL);
            while (!www.isDone)
            {

            }

            
            //var N = JSON.Parse(www.text);
            Debug.Log("ANNO FILE LENGTH BEFORE SPLIT: " + www.text.Length);
            strParsed = "{" + "\""+"data"+ "\""+":";//{"data":
            Debug.Log("STRPARSED: "+strParsed+" LENGTH: "+strParsed.Length);
            //tempMinus = strParsed.Length;
            //Debug.Log("STRPARSED USED IN SLPIT: "+strParsed.ToCharArray().ToString());

            //char[] = ","[0];

            //splitAnnoFile = www.text.Split(strParsed.ToCharArray());
            if (www.text.Length > 5)
            {
                annoFileToBeParsed = www.text.Substring(strParsed.Length + 1, www.text.Length - 1 - (strParsed.Length + 1));
                annoFileOrigin = annoFileToBeParsed;
                initialStringLength = annoFileToBeParsed.Length;

                hasAnno = true;
            }
            else
            {
                hasAnno = false;
            }
            //splitAnnoFile = www.text.Split(new string[] { strParsed}.ToString().ToCharArray());
            //splitAnnoFile = www.text.Split();

            //www.text.IndexOf(strParsed);
            //www.text.

            //Debug.Log("ANNO LENGTH AFTER SPLIT: "+annoFileToBeParsed.Length);
        }

        

        loadAnnoFileFinished = true;
        yield return 1f;
    }

}
