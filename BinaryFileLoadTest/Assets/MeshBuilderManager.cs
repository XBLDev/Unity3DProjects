using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MeshBuilderManager : MonoBehaviour {

    List<MeshBuilder> meshBuilderList;
    GameObject[] meshBuilders;
    public GameObject MeshBuilderPrefab;
    public int currentMeshCount = 0;
    //public Text textBoard;

    bool destroyingMeshes = false;
    // Use this for initialization
    void Start () {
        //meshBuilders = new GameObject[0];
	}
	
	// Update is called once per frame
	void Update () {

        if (destroyingMeshes)
        {

            Debug.Log("DESTORYING MESHES");
            //destroyMeshBuilders();
            StartCoroutine(destroyMeshBuildersDelayed());
        }



    }

    public void setDestroyingMesh(bool dm)
    {
        this.destroyingMeshes = dm;
    }

    public void addNewMesh()
    {

    }

    public void initialiseMeshList(int expectedMeshCount)
    {
        meshBuilders = new GameObject[expectedMeshCount];
        currentMeshCount = 0;
    }

    public void createNewMeshBuilder(Vector3[] positions, int[] triangles, Vector3[] normals, Texture2D meshtexture, Vector2[] uvs)
    {
        GameObject newMeshBuilder = (GameObject)Instantiate(MeshBuilderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newMeshBuilder.GetComponent<MeshBuilder>().buildNewMesh(positions, triangles, normals, meshtexture, uvs);
        //newMeshBuilder.transform.position = new Vector3(400f,400f,400f);

        if (Application.platform == RuntimePlatform.Android)
        {
            newMeshBuilder.gameObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        }
        //newMeshBuilder.gameObject.transform.localScale.Set(0.025f, 0.025f, 0.025f);

        //UPDATE MESHBUILDER LIST
        currentMeshCount++;
        meshBuilders[currentMeshCount-1] = newMeshBuilder;
        
        //meshBuilders[currentMeshCount-1] = newMeshBuilder;
        
    }

    public GameObject[] getMeshBuilders()
    {
        //for (int i = 0; i < meshBuilders.Length; i++)
        //{
        //    meshBuilders[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("",);
        //}
        return meshBuilders;
    }

    public int getMeshCount()
    {
        return currentMeshCount;
    }

    public void destroyMeshBuilders()
    {
        Debug.Log("DESTROY CALLED, MESH COUNT: "+meshBuilders.Length);
        int temp = meshBuilders.Length;

        for(int i =0;i<meshBuilders.Length;i++)
        {
            if (meshBuilders[i] != null)
            {
                GameObject mb = meshBuilders[i];
                //textBoard.text = "CLEANING MESH PART: " + currentMeshCount + "/" + temp;

                mb.GetComponent<MeshFilter>().mesh.Clear(false);
                mb.GetComponent<MeshFilter>().mesh = null;
                meshBuilders[i] = null;
                Destroy(mb, 1.0f);
            }
        }


        //foreach(GameObject mb in meshBuilders)
        //{
        //    //mb.setEmptyMesh();
        //    textBoard.text = "CLEANING MESH PART: "+currentMeshCount+"/"+temp;

        //    mb.GetComponent<MeshFilter>().mesh.Clear(false);
        //    mb.GetComponent<MeshFilter>().mesh = null;

        //    Destroy(mb,1.0f);
            
        //}
        currentMeshCount = 0;
        //System.GC.Collect();


        Debug.Log("DESTROY MESH COMPLETE");


    }



    public IEnumerator destroyMeshBuildersDelayed()
    {
        Debug.Log("DESTROY CALLED, MESH COUNT: " + meshBuilders.Length);
        int temp = meshBuilders.Length;

        for (int i = 0; i < meshBuilders.Length; i++)
        {
            if (meshBuilders[i] != null)
            {
                GameObject mb = meshBuilders[i];
                //textBoard.text = "CLEANING MESH PART: " + currentMeshCount + "/" + temp;

                mb.GetComponent<MeshFilter>().mesh.Clear(false);
                mb.GetComponent<MeshFilter>().mesh = null;
                meshBuilders[i] = null;
                Destroy(mb, 1.0f);
                yield return 10;
            }
        }


        //foreach(GameObject mb in meshBuilders)
        //{
        //    //mb.setEmptyMesh();
        //    textBoard.text = "CLEANING MESH PART: "+currentMeshCount+"/"+temp;

        //    mb.GetComponent<MeshFilter>().mesh.Clear(false);
        //    mb.GetComponent<MeshFilter>().mesh = null;

        //    Destroy(mb,1.0f);

        //}
        currentMeshCount = 0;

        Resources.UnloadUnusedAssets();

        System.GC.Collect();

        this.destroyingMeshes = false;
        Debug.Log("DESTROY MESH COMPLETE");

        
    }

}
