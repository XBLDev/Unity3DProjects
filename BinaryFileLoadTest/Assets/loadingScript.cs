using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class loadingScript : MonoBehaviour {

    public Transform LoadingBar;
    public Transform TextIndicator;
    public Transform TextLoading;
    [SerializeField]
    private float currentAmount = 0f;
    
    private float speed = 0.1f;
    public float targetAmount = 100f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(currentAmount<targetAmount)
        {
            currentAmount += speed * Time.deltaTime;
            TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "/"+targetAmount;
            TextLoading.gameObject.SetActive(true);
        }
        else
        {
            TextLoading.gameObject.SetActive(false);
            //TextIndicator.GetComponent<Text>().text = "DONE";

        }
        LoadingBar.GetComponent<Image>().fillAmount = currentAmount / targetAmount;
    }

    void setAmount()
    {
        LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;

    }
    void setCurrentAmount(float amount)
    {

    }

}
