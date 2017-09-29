using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMarker : MonoBehaviour {


    Vector3[] markerPositions;
    public GameObject markerPrefab;
	// Use this for initialization
	void Start () {

        int Zoffset = 0;
        if(Application.platform == RuntimePlatform.Android)
        {
            Zoffset = 50;
        }

        Vector3 temp1 = new Vector3(-7.1794974451509095f,3.156415500123171f,5.902114383359231f+Zoffset);
        Vector3 temp2 = new Vector3(10.033537254924921f, 8.303314199800639f, 4.968671517428904f + Zoffset);
        Vector3 temp3 = new Vector3(7.748372175146814f, 10.656259404822652f, -2.2439202033413697f + Zoffset);
        Vector3 temp4 = new Vector3(-8.001383918972818f, 5.793138029785052f, 0.23973144448183914f + Zoffset);
        Vector3 temp5 = new Vector3(-7.175041311929253f, 8.388606261732555f, 0.643546549340777f + Zoffset);
        Vector3 temp6 = new Vector3(-7.836320529784045f, 5.8782355778461115f, 0.5819819446073744f + Zoffset);
        Vector3 temp7 = new Vector3(8.119224720131573f,  28.16740862045088f, -2.1847606121682293f + Zoffset);

        markerPositions = new Vector3[7];
        markerPositions[0] = temp1;
        markerPositions[1] = temp2;
        markerPositions[2] = temp3;
        markerPositions[3] = temp4;
        markerPositions[4] = temp5;
        markerPositions[5] = temp6;
        markerPositions[6] = temp7;

        for(int i=0;i<markerPositions.Length;i++)
        {
            GameObject marker = (GameObject)Instantiate(markerPrefab, markerPositions[i], Quaternion.identity);
            //marker.gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
