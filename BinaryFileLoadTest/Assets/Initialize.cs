using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Initialize : MonoBehaviour {

    string baseURLPath = "https://ie.csiro.au/services/p3d-legacy/models/";
    string posEndURL = "/geometry/position";
    string normalEndURL = "/geometry/normal";
    string uvEndURL = "/geometry/uv";
    string textureEndURL = "/texture/hi";
    string annotationEndURL = "/annotations";

    bool summaryFileLoaded = false;
    bool modelNamesLoaded = false;
    bool modelIDsLoaded = false;
    bool modelsLoaded = false;
    bool urlsInitialised = false;
    bool firstModelLoaded = false;
    string[] firstline;
    string[] bugInfo;



    int currentBug = 3;//3 is the 4th bug with anno, 5th bug has big anno file
    int totalBugNum = 0;
    string[] names;
    string[] ids;
    List<string> nameList;
    List<string> idlist;
    List<string[]> urls;

    string modelUrl = "https://ie.csiro.au/services/p3d-legacy/models/";
    public InputField nameField;

    // Use this for initialization
    void Start () {
        urls = new List<string[]>();
        nameList = new List<string>();
        idlist = new List<string>();
	}
	



	// Update is called once per frame
	void Update () {
        if(!summaryFileLoaded)
        {
            StartCoroutine(loadSummaryFile());
        }
        else if(summaryFileLoaded && ! modelsLoaded)
        {
            StartCoroutine(loadIndividualBugInfo());

        }
        else if (modelsLoaded && ! modelNamesLoaded)
        {
            StartCoroutine(loadIndividualBugName());

        }
        else if (modelNamesLoaded && !modelIDsLoaded)
        {
            StartCoroutine(loadIndividualBugID());

        }
        else if (modelIDsLoaded && !urlsInitialised)
        {
            StartCoroutine(initializeURLs());

        }

    }


    public void nextBug()
    {

        if(!firstModelLoaded)
        {

            //GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().destroyMeshBuildersDelayed();
            nameField.text = nameList[currentBug-1];

            string[] nextBugUrl = urls[currentBug-1];
            string pos = nextBugUrl[0];
            string normal = nextBugUrl[1];
            string uv = nextBugUrl[2];
            string tex = nextBugUrl[3];
            string anno = nextBugUrl[4];
            //string pos, string nor, string uv, string hi, string anno
            GameObject.FindGameObjectsWithTag("FileReader")[0].GetComponent<testConnect>().setCurrentModelURLS(pos,normal,uv, tex, anno);
            GameObject.FindGameObjectsWithTag("annotationManager")[0].GetComponent<annotationManager>().startParsingAnnotationFile(anno);

            Debug.Log("ANNO URL IS: "+anno);

            if (currentBug+1 > totalBugNum)
            {
                currentBug = 1;
            }
            else
            {
                currentBug++;
            }

            //GameObject.FindGameObjectsWithTag("FileReader")[0].GetComponent<testConnect>().setLoadingModelComplete(false);

            firstModelLoaded = true;
            Debug.Log("FIRST MODEL LOADED");
        }

        else if (GameObject.FindGameObjectsWithTag("FileReader")[0].GetComponent<testConnect>().getLoadingModelComplete())
        {
            //GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().setDestroyingMesh(true);
            //StartCoroutine(GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().destroyMeshBuildersDelayed());

            GameObject.FindGameObjectsWithTag("annotationManager")[0].GetComponent<annotationManager>().clearCurrentAnno();

            GameObject.FindGameObjectsWithTag("meshBuilderManager")[0].GetComponent<MeshBuilderManager>().destroyMeshBuilders();
            Resources.UnloadUnusedAssets();

            nameField.text = nameList[currentBug - 1];

            string[] nextBugUrl = urls[currentBug-1];
            string pos = nextBugUrl[0];
            string normal = nextBugUrl[1];
            string uv = nextBugUrl[2];
            string tex = nextBugUrl[3];
            string anno = nextBugUrl[4];

            GameObject.FindGameObjectsWithTag("FileReader")[0].GetComponent<testConnect>().setCurrentModelURLS(pos, normal, uv, tex, anno);

            GameObject.FindGameObjectsWithTag("annotationManager")[0].GetComponent<annotationManager>().startParsingAnnotationFile(anno);
            GameObject.FindGameObjectsWithTag("annotationManager")[0].GetComponent<annotationManager>().setLoadAnnoFileFinished(false);

            Debug.Log("ANNO URL IS: " + anno);

            if (currentBug + 1 > totalBugNum)
            {
                currentBug = 1;
            }
            else
            {
                currentBug++;
            }
            Debug.Log("MODEL LOADED, MODEL NUMBER: "+currentBug);
            //GameObject.FindGameObjectsWithTag("FileReader")[0].GetComponent<testConnect>().setLoadingModelComplete(false);
        }

    }

    IEnumerator initializeURLs()
    {


        foreach(string id in idlist)
        {
            string posString = baseURLPath + id + posEndURL;
            string normalString = baseURLPath + id + normalEndURL;
            string uvString = baseURLPath + id + uvEndURL;
            string textureString = baseURLPath + id + textureEndURL;
            string annotationString = baseURLPath + id + annotationEndURL;

            string[] current = new string[5];
            current[0] = posString;
            current[1] = normalString;
            current[2] = uvString;
            current[3] = textureString;
            current[4] = annotationString;

            urls.Add(current);
        }

        urlsInitialised = true;
        Debug.Log("URL INITIALIZED, SIZE: "+urls.Count);

        yield return 10f;
    }

    IEnumerator loadIndividualBugID()
    {
        for (int i = 0; i < bugInfo.Length; i++)
        {
            //Debug.Log(bugInfo[i]);

            string temp = bugInfo[i];

            string id = temp.Substring(temp.IndexOf("id"));
            id = id.Substring(id.IndexOf(":")+2, id.Length - (id.IndexOf(":") + 2)-1);

            idlist.Add(id);
            Debug.Log("ID: "+id);
        }
        modelIDsLoaded = true;
        yield return 10f;
    }

    IEnumerator loadIndividualBugName()
    {

        //[{"name":"Amycterine Ground Weevil (Local)","

        for (int i = 0; i < bugInfo.Length; i++)
        {
            //Debug.Log(bugInfo[i]);

            string temp = bugInfo[i];
            string name = "";
            int commaPosition = temp.IndexOf(",");
            if (commaPosition >= 0)
            {
                 name = temp.Substring(0, temp.IndexOf(","));
                 //name = name.Substring(name.LastIndexOf(":")+1,name.Length-2);
            }
            string final = "";
            if (name.Length > 0)
            {
                final = name.Substring(7+2 , name.Length -9-1);
            }
            nameList.Add(final);
            
            Debug.Log("NAME: "+final);
        }
        modelNamesLoaded = true;

        yield return 10f;
    }


    IEnumerator loadIndividualBugInfo()
    {


        firstline[0] = firstline[0].Substring(1, firstline[0].Length-3);
        bugInfo = firstline[0].Split("}"[0]);
        



        for (int i = 0; i < bugInfo.Length; i++)
        {
            if(bugInfo[i].StartsWith(","))
            {
                bugInfo[i] = bugInfo[i].Substring(1,bugInfo[i].Length-1);
            }
            totalBugNum++;
            Debug.Log(bugInfo[i]);
        }
        modelsLoaded = true;
        yield return 10f;
    }

    IEnumerator loadSummaryFile()
    {
        WWW www = new WWW(modelUrl);
        while (!www.isDone)
        {

        }

        firstline = www.text.Split("\n"[0]);
        Debug.Log("SUMMARY FILE, LINE NUMBER: " + firstline.Length);

        summaryFileLoaded = true;
        yield return 10f;
    }
}
