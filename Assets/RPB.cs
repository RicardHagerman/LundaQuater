using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RPB : MonoBehaviour
{

    public Transform loadingBar;
    public Transform textIndicator;
    public Transform textLoading;

    [SerializeField]
    float currentAmaount;
    [SerializeField]
    float speed;


    void Start()
    {
        textLoading.gameObject.SetActive(false);
    }


    void Update()
    {
        if (currentAmaount < 100)
        {
            currentAmaount += speed * Time.deltaTime;
            textIndicator.GetComponent<Text>().text = "" + (int)currentAmaount + " %";
            textLoading.gameObject.SetActive(true);
        }
        else
        {
            textLoading.gameObject.SetActive(false);
            textIndicator.GetComponent<Text>().text = "Done";
        }
        loadingBar.GetComponent<Image>().fillAmount = currentAmaount / 100;
    }
}
