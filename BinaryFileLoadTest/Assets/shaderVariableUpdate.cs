using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shaderVariableUpdate : MonoBehaviour {



    public GameObject meshBuilderManagerObj;
    public InputField mousePositionField;

    Vector4[] annotationFaces;
    bool isInEditMode = false;
    bool isInLineDrawingMode = false;
    bool isInFaceDrawingMode = false;
    // Use this for initialization
    void Start() {
        annotationFaces = new Vector4[] { };
    }

    // Update is called once per frame
    void Update() {
        //Input.mousePosition

        //GameObject[] meshbuilders = meshBuilderManagerObj.gameObject.GetComponent<MeshBuilderManager>().getMeshBuilders();

        //for(int i=0; i<meshbuilders.Length;i++)
        //{
        //    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositionx", Input.mousePosition.x);
        //    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositiony", Input.mousePosition.y);

        //}

        updateShaderUniform();


    }

    public void setIsInLineMode()
    {
        //setAButtonIsTouched(true);
        this.isInLineDrawingMode = true;
        this.isInFaceDrawingMode = false;


        LineRenderer lr =
        linerenderer.gameObject.GetComponent<LineRenderer>();

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

        if (ClearAllLinesButton)
        {
            ClearAllLinesButton.gameObject.SetActive(true);
        }

        if (ClearPreviousLineButton)
        {
            ClearPreviousLineButton.gameObject.SetActive(true);
        }

        if(addLineButton)
        {
            addLineButton.gameObject.SetActive(true);
        }

        if (faceDrawingBrushButton)
        {
            faceDrawingBrush = false;
            faceDrawingBrushButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            faceDrawingBrushButton.gameObject.SetActive(false);
        }

        if (faceDrawingEraserButton)
        {
            faceDrawingEraser = false;
            faceDrawingEraserButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);

            faceDrawingEraserButton.gameObject.SetActive(false);
        }

        if(eraseAllDrawnFacesButton)
        {
            eraseAllDrawnFacesButton.gameObject.SetActive(false);

        }

        if (drawConfirmButton)
        {
            drawConfirmButton.gameObject.SetActive(false);

        }

        if (lineModeButton && faceModeButton)
        {
            lineModeButton.gameObject.GetComponent<Image>().color = new Color(0, 255, 213);
            faceModeButton.gameObject.GetComponent<Image>().color = new Color(0, 0, 0);

        }
        //setAButtonIsTouched(false);
    }


    List<Vector4> faceDrawnPoints = new List<Vector4>();

    public void setIsInFaceMode()
    {
        //setAButtonIsTouched(true);
        this.isInLineDrawingMode = false;
        this.isInFaceDrawingMode = true;

        linerenderer.gameObject.GetComponent<LineRenderer>().positionCount = 0;
        numOfPointsBeginDrawn = 0;
        addCurrentPointToLineBeingDrawn = false;

        if (lineModeButton && faceModeButton)
        {
            lineModeButton.gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
            faceModeButton.gameObject.GetComponent<Image>().color = new Color(0, 255, 213);

        }

        if (faceDrawingBrushButton)
        {
            faceDrawingBrushButton.gameObject.SetActive(true);
        }

        if (faceDrawingEraserButton)
        {
            faceDrawingEraserButton.gameObject.SetActive(true);
        }

        if(eraseAllDrawnFacesButton)
        {
            eraseAllDrawnFacesButton.gameObject.SetActive(true);

        }

        if(drawConfirmButton)
        {
            drawConfirmButton.gameObject.SetActive(true);

        }

        if (ClearAllLinesButton)
        {
            ClearAllLinesButton.gameObject.SetActive(false);
        }

        if (ClearPreviousLineButton)
        {
            ClearPreviousLineButton.gameObject.SetActive(false);
        }

        if(addLineButton)
        {
            addLineButton.gameObject.SetActive(false);
        }
        //setAButtonIsTouched(false);
    }


    public Button lineModeButton;
    public Button faceModeButton;
    public Button EditModeButton;
    public Button ClearAllLinesButton;
    public Button ClearPreviousLineButton;
    public Button faceDrawingBrushButton;
    public Button faceDrawingEraserButton;
    public Button eraseAllDrawnFacesButton;
    public Button addLineButton;
    public Button drawConfirmButton;
    public Button saveEditButton;
    public GameObject linerenderer;


    public void saveCurrentEdit()
    {
        
        if(isInLineDrawingMode)
        {

        }
        if(isInFaceDrawingMode)
        {

        }
    }

    public void setIsInEditMode()
    {
        this.isInEditMode = !this.isInEditMode;

        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 0);
        if (linerenderer)
        {
            linerenderer.gameObject.GetComponent<LineRenderer>().positionCount = 0;

            linerenderer.gameObject.GetComponent<LineRenderer>().startColor = new Color(0, 255, 0, 255);
            linerenderer.gameObject.GetComponent<LineRenderer>().endColor = new Color(0, 255, 0, 255);
            linerenderer.gameObject.GetComponent<LineRenderer>().useWorldSpace = false;

        }

        if (isInEditMode)
        {
            isInLineDrawingMode = false;
            isInFaceDrawingMode = false;


            lineModeButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            faceModeButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);

            GameObject[] meshbuilders =
            GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].
            GetComponent<MeshBuilderManager>().getMeshBuilders();

            for (int i = 0; i < meshbuilders.Length; i++)
            {
                GameObject mb = meshbuilders[i];
                mb.gameObject.GetComponent<MeshCollider>().sharedMesh = mb.gameObject.GetComponent<MeshFilter>().mesh;
                mb.gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 0);
                mb.gameObject.GetComponent<MeshRenderer>().material.SetInt("isInEditMode", 1);
            }


            if (EditModeButton)
            {
                EditModeButton.gameObject.GetComponentInChildren<Text>().text = "NORMALMODE";
            }

            if (lineModeButton)
            {
                if (!lineModeButton.gameObject.activeSelf)
                {
                    lineModeButton.gameObject.SetActive(true);
                }

            }
            if (faceModeButton)
            {
                if (!faceModeButton.gameObject.activeSelf)
                {
                    faceModeButton.gameObject.SetActive(true);
                }
            }
            if(saveEditButton)
            {
                saveEditButton.gameObject.SetActive(true);
            }
        }
        else
        {
            isInLineDrawingMode = false;
            isInFaceDrawingMode = false;
            faceDrawingBrush = false;
            faceDrawingEraser = false;
            addCurrentPointToLineBeingDrawn = false;
            drawingConfirmed = false;
            faceDrawnPoints = new List<Vector4>();

            lineModeButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
            faceModeButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);


            GameObject[] meshbuilders =
            GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].
            GetComponent<MeshBuilderManager>().getMeshBuilders();

            for (int i = 0; i < meshbuilders.Length; i++)
            {
                GameObject mb = meshbuilders[i];
                mb.gameObject.GetComponent<MeshCollider>().sharedMesh = null;
                mb.gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 0);
                mb.gameObject.GetComponent<MeshRenderer>().material.SetInt("isInEditMode", 0);
            }

            if (EditModeButton)
            {
                EditModeButton.gameObject.GetComponentInChildren<Text>().text = "EDIT";
            }

            if (lineModeButton)
            {
                lineModeButton.gameObject.SetActive(false);
            }

            if (faceModeButton)
            {
                faceModeButton.gameObject.SetActive(false);
            }

            if (ClearAllLinesButton)
            {
                ClearAllLinesButton.gameObject.SetActive(false);
            }

            if (ClearPreviousLineButton)
            {
                ClearPreviousLineButton.gameObject.SetActive(false);
            }

            if (faceDrawingBrushButton)
            {
                faceDrawingBrushButton.gameObject.SetActive(false);

            }

            if (faceDrawingEraserButton)
            {
                faceDrawingEraserButton.gameObject.SetActive(false);

            }

            if(eraseAllDrawnFacesButton)
            {
                eraseAllDrawnFacesButton.gameObject.SetActive(false);
            }

            if(addLineButton)
            {
                addLineButton.gameObject.SetActive(false);

            }

            if(drawConfirmButton)
            {
                drawConfirmButton.gameObject.SetActive(false);
            }

            if (saveEditButton)
            {
                saveEditButton.gameObject.SetActive(false);
            }

        }
    }

    public void setAnnotationFaces(Vector4[] value, int size)
    {
        this.annotationFaces = new Vector4[size];
        if (size != 0)
        {
            for (int i = 0; i < loopNumber; i++)
            {
                annotationFaces[i] = value[i];
            }
        }
    }

    bool hasAnnotation = false;
    public void setHasAnnotations(bool value)
    {
        this.hasAnnotation = value;
    }



    //public Material meshMat;
    int count = 0;
    public InputField annotationProcessStatusField;
    bool shaderSetReady = false;
    bool aButtonIsTouched = false;
    int loopNumber = 0;
    public void setLoopNumber(int value)
    {
        this.loopNumber = value;
    }


    public void setAddCurrentPointToLineBeginDrawn()
    {
        //setAButtonIsTouched(true);
        this.addCurrentPointToLineBeingDrawn = true;
        //setAButtonIsTouched(false);
    }

    public void clearAllLinesDrew()
    {
        //setAButtonIsTouched(true);
        if (linerenderer)
        {
            linerenderer.gameObject.GetComponent<LineRenderer>().positionCount = 0;
            numOfPointsBeginDrawn = 0;
            addCurrentPointToLineBeingDrawn = false;
        }
        //setAButtonIsTouched(false);
    }

    void setAButtonIsTouched(bool value)
    {
        this.aButtonIsTouched = value;

    }

    public void clearLastLineDrew()
    {
        //setAButtonIsTouched(true);
        if (linerenderer)
        {
            if (linerenderer.gameObject.GetComponent<LineRenderer>().positionCount >= 1)
            {
                linerenderer.gameObject.GetComponent<LineRenderer>().positionCount -= 1;
                numOfPointsBeginDrawn -= 1;
                addCurrentPointToLineBeingDrawn = false;
            }
        }
        //setAButtonIsTouched(false);
    }

    bool addCurrentPointToLineBeingDrawn = false;
    bool faceDrawingBrush = false;
    public void setFaceDrawingBrushMode()
    {
        //setAButtonIsTouched(true);
        faceDrawingBrush = true;
        faceDrawingEraser = false;

        faceDrawingBrushButton.gameObject.GetComponent<Image>().color = new Color(0, 255, 231);
        faceDrawingEraserButton.gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
        //setAButtonIsTouched(false);
    }

    bool faceDrawingEraser = false;

    public void setFaceDrawingEraserMode()
    {
        //setAButtonIsTouched(true);
        faceDrawingBrush = false;
        faceDrawingEraser = true;

        faceDrawingBrushButton.gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
        faceDrawingEraserButton.gameObject.GetComponent<Image>().color = new Color(0, 255, 231);
        //setAButtonIsTouched(false);
    }

    public void eraseAllDrawnFaces()
    {
        //setAButtonIsTouched(true);
        faceDrawnPoints.Clear();
        //setAButtonIsTouched(false);
    }


    int numOfPointsBeginDrawn = 0;
    public InputField rayCastStatusField;
    bool drawingConfirmed = false;

    public void setDrawingConfirmed()
    {
        //setAButtonIsTouched(true);
        drawingConfirmed = true;
        //setAButtonIsTouched(false);
    }

    private void updateShaderUniform()
    {

        //meshMat.SetFloat("mousePositiony", Input.mousePosition.y);
        //meshMat.SetFloat("screenHeight", Screen.height);
        if (Application.platform != RuntimePlatform.Android)//ON COMPUTER
        {
            GameObject[] meshbuilders = GameObject.FindGameObjectsWithTag("meshBuilder");
            if (!isInEditMode)//NO IN EDIT MODE, VIEW ANNOTATION MODE
            {
                for (int i = 0; i < meshbuilders.Length; i++)
                {

                    //Debug.Log
                    //    (meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.GetFloat("currentSelectedPointX") );


                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositiony", Input.mousePosition.y);
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenHeight", Screen.height);

                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositionx", Input.mousePosition.x);
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenWidth", Screen.width);

                    if (annotationFaces.Length > 0)
                    {
                        //Vector3 pos1 = this.annotationFaces[0];
                        //Vector3 pos2 = this.annotationFaces[1];
                        //Vector3 pos3 = this.annotationFaces[2];
                        //Vector3 pos4 = this.annotationFaces[3];

                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos1", pos1);
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos2", pos2);
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos3", pos3);
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos4", pos4);

                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("annotationFaces");
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("lengthOfAnnotation", this.loopNumber);

                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVectorArray("annotationFaces", this.annotationFaces);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 1);

                        //shaderSetReady = true;
                        if (annotationProcessStatusField)
                        {
                            annotationProcessStatusField.text = "facesPassed";
                        }
                    }
                    else
                    {
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("annotationFaces");
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 0);
                        if (annotationProcessStatusField)
                        {
                            //annotationProcessStatusField.text = "nofacepassed";
                        }
                    }
                }



                if (mousePositionField)
                {
                    mousePositionField.text = "x:" + Input.mousePosition.x + " y:" + Input.mousePosition.y + " z:" + Input.mousePosition.z;

                }
            }

            else//IN EDIT MODE
            {

                //GameObject[] meshbuilders = GameObject.FindGameObjectsWithTag("meshBuilder");

                    for (int i = 0; i < meshbuilders.Length; i++)
                    {
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositiony", Input.mousePosition.y);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenHeight", Screen.height);

                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositionx", Input.mousePosition.x);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenWidth", Screen.width);
                        if(isInLineDrawingMode)
                        {
                            meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInLineMode", 1);
                            meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInFaceMode", 0);

                        }
                        if (isInFaceDrawingMode)
                        {
                            Vector4[] temp = new Vector4[500];
                            this.faceDrawnPoints.CopyTo(temp);
                            //Vector4[] tempV4 = temp;
                            meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVectorArray("faceDrawingPositions", temp);


                            meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("lengthOfFaceDrawnPassed", faceDrawnPoints.Count);
                            meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInFaceMode", 1);
                            meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInLineMode", 0);
                        }

                    }

                    if(isInLineDrawingMode)
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit))
                        {
                        //Debug.Log("HIT SOMETHING");
                            //Transform objectHit = hit.transform;
                            Vector3 hitpoint = hit.point;
                            // Do something with the object that was hit by the raycast.
                            if (Input.GetMouseButtonDown(0))
                            {
                                Debug.Log
                                ("HIT: x: "+ hitpoint.x +
                                ", y: "+ hitpoint.y +
                                ", z: "+ hitpoint.z);
                                this.addCurrentPointToLineBeingDrawn = true;
                            }

                            if(addCurrentPointToLineBeingDrawn)
                            
                            {
                                if(linerenderer)
                                {
                                    linerenderer.gameObject.GetComponent<LineRenderer>().positionCount += 1;
                                    linerenderer.gameObject.GetComponent<LineRenderer>().SetPosition(numOfPointsBeginDrawn, hitpoint);
                                    numOfPointsBeginDrawn++;
                                }
                                addCurrentPointToLineBeingDrawn = false;
                            }
                        }
                        else
                        {
                            //Debug.Log("HIT NOTHING");

                        }
                    }

                    if(isInFaceDrawingMode)
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 hitpoint = hit.point;
                            if(faceDrawingBrush)
                            {
                                if (Input.GetMouseButtonDown(0))
                                {
                                    this.faceDrawnPoints.Add(hitpoint);
                                }
                            }
                            if(faceDrawingEraser)
                            {
                                if (Input.GetMouseButtonDown(0) && faceDrawnPoints.Count > 0)
                                {
                                    //this.faceDrawnPoints.Add(hitpoint);
                                    Vector4 pointWithShortestDistanceToEraser = new Vector4();
                                    int index = 0;
                                    float mindistance = 100000000f;
                                    for(int i = 0; i <faceDrawnPoints.Count;i++)
                                    {
                                        Vector3 currentDrawnFacePoint = faceDrawnPoints[i];
                                        float distance = Vector3.Distance(hitpoint, currentDrawnFacePoint);
                                        if(distance < mindistance)
                                        {
                                            mindistance = distance;
                                            index = i;
                                        }
                                    }
                                    pointWithShortestDistanceToEraser = faceDrawnPoints[index];
                                    faceDrawnPoints.Remove(pointWithShortestDistanceToEraser);
                                    //faceDrawnPoints
                                }
                             }
                        }
                    }
                
            }
        }
        else// ON TANGO PHONE
        {
            GameObject[] meshbuilders = GameObject.FindGameObjectsWithTag("meshBuilder");
            if (!isInEditMode)//NO IN EDIT MODE, VIEW ANNOTATION MODE
            {
                for (int i = 0; i < meshbuilders.Length; i++)
                {
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositiony", Screen.height / 2);
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenHeight", Screen.height);

                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositionx", Screen.width / 2);
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenWidth", Screen.width);

                    if (annotationFaces.Length > 0)
                    {
                        //Vector3 pos1 = this.annotationFaces[0];
                        //Vector3 pos2 = this.annotationFaces[1];
                        //Vector3 pos3 = this.annotationFaces[2];
                        //Vector3 pos4 = this.annotationFaces[3];

                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos1", pos1);
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos2", pos2);
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos3", pos3);
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("annotationFacePos4", pos4);

                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.

                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("lengthOfAnnotation", this.loopNumber);

                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVectorArray("annotationFaces", this.annotationFaces);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 1);
                        if (annotationProcessStatusField)
                        {
                            annotationProcessStatusField.text = "facesPassed";
                        }
                    }
                    else
                    {
                        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("annotationFaces");
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("hasAnnotationFaces", 0);
                        if (annotationProcessStatusField)
                        {
                            //annotationProcessStatusField.text = "nofacepassed";
                        }
                    }
                    //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.setv
                }

                if (mousePositionField)
                {
                    mousePositionField.text = "UpdatingScreenCenter";

                }
            }
            else//IN EDIT MODE
            {
                for (int i = 0; i < meshbuilders.Length; i++)
                {
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositiony", Screen.height / 2);
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenHeight", Screen.height);

                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositionx", Screen.width / 2);
                    meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenWidth", Screen.width);

                    if (isInLineDrawingMode)
                    {
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInLineMode", 1);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInFaceMode", 0);

                    }
                    if (isInFaceDrawingMode)
                    {
                        Vector4[] temp = new Vector4[500];
                        this.faceDrawnPoints.CopyTo(temp);
                        //Vector4[] tempV4 = temp;
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetVectorArray("faceDrawingPositions", temp);


                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("lengthOfFaceDrawnPassed", faceDrawnPoints.Count);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInFaceMode", 1);
                        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("isInLineMode", 0);
                    }

                }

                if (isInLineDrawingMode)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

                    if(Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began) 
                        && Input.GetTouch(0).position.x > saveEditButton.GetComponent<RectTransform>().rect.width * (Screen.width/800))
                    {
                        //Input.GetTouch(0).position.x
                        ray = Camera.main.ViewportPointToRay
                            (
                            new Vector3(
                            Input.GetTouch(0).position.x / Screen.width,
                            Input.GetTouch(0).position.y / Screen.height, 
                            0)
                            );
                        addCurrentPointToLineBeingDrawn = true;
                        //setAButtonIsTouched(false);
                    }
                    //ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));


                    //var cam : Transform = Camera.main.transform;
                    Ray camray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    //var hit : RaycastHit;
                    //if (Physics.Raycast(ray, hit, 500))


                    if (Physics.Raycast(ray, out hit))
                    {
                        if(rayCastStatusField)
                        {
                            rayCastStatusField.text = "RAYHIT";
                        }
                        //Debug.Log("HIT SOMETHING");
                        //Transform objectHit = hit.transform;
                        Vector3 hitpoint = hit.point;

                        // Do something with the object that was hit by the raycast.
                        //if (Input.GetMouseButtonDown(0))
                        //if(Input.touchCount > 0 && GUIUtility.hotControl == 0)
                        //{
                        //    //Debug.Log
                        //    //("HIT: x: " + hitpoint.x +
                        //    //", y: " + hitpoint.y +
                        //    //", z: " + hitpoint.z);
                        //    this.addCurrentPointToLineBeingDrawn = true;
                        //}
                        //if(Input.touch)
                        //{

                        //}
                        if (addCurrentPointToLineBeingDrawn)

                        {
                            if (linerenderer)
                            {
                                linerenderer.gameObject.GetComponent<LineRenderer>().positionCount += 1;
                                linerenderer.gameObject.GetComponent<LineRenderer>().SetPosition(numOfPointsBeginDrawn, hitpoint);
                                numOfPointsBeginDrawn++;
                            }
                            addCurrentPointToLineBeingDrawn = false;
                        }
                    }
                    else
                    {
                        if (rayCastStatusField)
                        {
                            rayCastStatusField.text = "RAYNOTHIT";
                        }
                        //Debug.Log("HIT NOTHING");

                    }
                }

                if (isInFaceDrawingMode)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Ray camray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);



                    if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began)
                        && Input.GetTouch(0).position.x > saveEditButton.GetComponent<RectTransform>().rect.width * (Screen.width / 800))
                    {
                        //Input.GetTouch(0).position.x
                        ray = Camera.main.ViewportPointToRay
                            (
                            new Vector3(
                            Input.GetTouch(0).position.x / Screen.width,
                            Input.GetTouch(0).position.y / Screen.height,
                            0)
                            );
                        drawingConfirmed = true;
                        //setAButtonIsTouched(false);
                    }


                    if (Physics.Raycast(ray, out hit))
                    {
                        if (rayCastStatusField)
                        {
                            rayCastStatusField.text = "RAYHIT";
                        }
                        Vector3 hitpoint = hit.point;
                        if (faceDrawingBrush)
                        {
                            if (drawingConfirmed)
                            {

                                //if (rayCastStatusField)
                                //{
                                //    rayCastStatusField.text = "ADDFACEPOINT";
                                //}
                                //hitpoint.Scale(new Vector3(0.025f,0.025f,0.025f));
                                
                                this.faceDrawnPoints.Add(hitpoint);
                                drawingConfirmed = false; 
                            }
                        }
                        if (faceDrawingEraser)
                        {
                            if (drawingConfirmed && faceDrawnPoints.Count > 0)
                            {
                                //this.faceDrawnPoints.Add(hitpoint);
                                Vector4 pointWithShortestDistanceToEraser = new Vector4();
                                int index = 0;
                                float mindistance = 100000000f;
                                for (int i = 0; i < faceDrawnPoints.Count; i++)
                                {
                                    Vector3 currentDrawnFacePoint = faceDrawnPoints[i];
                                    float distance = Vector3.Distance(hitpoint, currentDrawnFacePoint);
                                    if (distance < mindistance)
                                    {
                                        mindistance = distance;
                                        index = i;
                                    }
                                }
                                pointWithShortestDistanceToEraser = faceDrawnPoints[index];
                                faceDrawnPoints.Remove(pointWithShortestDistanceToEraser);
                                //if (rayCastStatusField)
                                //{
                                //    rayCastStatusField.text = "REMOVEFACEPOINT";
                                //}

                                //drawingConfirmed = false;
                                //faceDrawnPoints
                            }
                            drawingConfirmed = false;
                        }
                    }
                    else
                    {
                        if (rayCastStatusField)
                        {
                            rayCastStatusField.text = "RAYNOTHIT";
                        }
                    }
                }


            }
        }
        //if (meshBuilderManagerObj.gameObject.GetComponent<MeshBuilderManager>().getMeshCount() > 0)
        //{
        //    GameObject[] meshbuilders = meshBuilderManagerObj.gameObject.GetComponent<MeshBuilderManager>().getMeshBuilders();
        //    mousePositionField.text = meshBuilderManagerObj.gameObject.GetComponent<MeshBuilderManager>().getMeshBuilders().Length.ToString();

        //    for (int i = 0; i < meshbuilders.Length; i++)
        //   // for (int i = 0; i < 1; i++)

        //    {

        //        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.setf

        //        meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetInt("mousePositionx", (int)Input.mousePosition.x);
        //        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("mousePositiony", Input.mousePosition.y);
        //        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenWidth", (float)Screen.width);
        //        //meshbuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("screenHeight", (float)Screen.height);

        //    }
        //}
    }
}
