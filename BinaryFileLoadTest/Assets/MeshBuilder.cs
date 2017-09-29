using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshBuilder : MonoBehaviour {



    private Mesh mesh;
    //private List<Mesh> meshes;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        //this.transform.Rotate(new Vector3(0, 1, 0), 1f);

        //if(mesh!=null)
        //   {

            //   }
    }

    public void setEmptyMesh()
    {
        mesh = null;
    }

    public Mesh getMesh()
    {
        return mesh;
    }

    public Material BugMatForPC;
    public Material BugMatForAndroid;

    public void buildNewMesh(Vector3[] positions, int[] triangles, Vector3[] normals, Texture2D meshtexture, Vector2[] uvs)
    {
        mesh = new Mesh();
        if (positions != null)
        {
            mesh.vertices = positions;
        }

        if (triangles != null)
        {
            mesh.triangles = triangles;
        }
        //mesh.colors = colors;
        if (normals != null)
        {
            mesh.normals = normals;
        }

        if (uvs != null)
        {
            mesh.uv = uvs;
        }
        //mesh.no
        //List
        //List<Color> colorlist = new List<Color>();
        //for(int i =0; i<colors.Length;i++)
        //{
        //    colorlist.Add(colors[i]);
        //}
        //mesh.SetColors(colorlist);

        //;
        //mesh.re
        if (normals == null)
        {
            mesh.RecalculateNormals();
        }
        mesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = mesh;
        //GetComponent<MeshCollider>().sharedMesh = mesh;
        //GetComponent<MeshFilter>().mesh.
        //GetComponent<MeshFilter>().

        if(Application.platform == RuntimePlatform.Android)
        {
            if(BugMatForAndroid)
            {
                GetComponent<MeshRenderer>().material = BugMatForAndroid;
            }
        }
        else
        {
            if (BugMatForPC)
            {
                GetComponent<MeshRenderer>().material = BugMatForPC;
            }
        }
        //GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Diffuse"));
        //GetComponent<MeshRenderer>().material.color = Color.blue;
        if (meshtexture != null)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                //Shader shader1 = Resources.Load("EnvironmentalLightingStandard", typeof(Shader)) as Shader;

                //GetComponent<MeshRenderer>().material.shader = Shader.Find("EnvironmentalLightingStandard");
                //GetComponent<MeshRenderer>().material.shader = shader1;
            }
            GetComponent<MeshRenderer>().material.mainTexture = meshtexture;

        }
        //else
        //{

        //    //GetComponent<MeshRenderer>().material.color = Color.blue;
        //}
        
        //Shader.
        //         mesh_renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));
        //GetComponent<MeshRenderer>().material.SetColorArray("Diffuse", colors);

       Debug.Log("MESH BUILDER: BUILT NEW MESH");
    }
}
