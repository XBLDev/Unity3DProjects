using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonTrigger : MonoBehaviour
{


    //public InputField temp;

    public void next()
    {


        GameObject.FindGameObjectsWithTag("Initializer")[0].GetComponent<Initialize>().nextBug();

        //Debug.Log("BUTTON TOUCHED");
        //temp.text = "BUTTON TOUCHED";
    }
}
