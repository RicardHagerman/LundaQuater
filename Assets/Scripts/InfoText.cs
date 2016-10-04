using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    Coroutine co;

    [SerializeField]
    Text infoText1;
    [SerializeField]
    Text infoText2;
    [SerializeField]
    GameObject swipeIcon;

    string infoText = "";

    void OnEnable()
    {
        swipeIcon.SetActive(false);
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
        co = StartCoroutine(InfoCO());
    }


    void OnDisable()
    {
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }


    IEnumerator InfoCO()
    {
        if (QuaterBack.SinglePlayer || QuaterBack.Challenge)
        {
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    infoText = "hur många kan du få i ?";
                    break;
                case 1:
                    infoText = "How many coins can you bounce into the beer??";
                    break;
                case 2:
                    infoText = "Boburorpop";
                    break;
            }
        }
        else
        {
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    infoText = "Studsa myntet på border, ner i ölglaset\nSätt ett finger på myntest och dra neråt\nJu längre du drar desto hårdare studsar myntet";
                    break;
                case 1:
                    infoText = "Bounce the coin off the table, into the beer.\nPut your finger on the coin and swipe down.\nThe longer the swipe the greater the bounce.";
                    break;
                case 2:
                    infoText = "Lolunondodabobrorygoggogerorietot äror dodetot bobäsostota\ngoglolugoggog goololugoggog goglolugoggog\naaaaaaaaaaaaaaaaaaaaaa";
                    break;
            }
        }

        for (int i = 0; i < infoText.Length + 1; i++)
        {
            infoText1.text = "" + infoText.Substring(0, i) + "";
            infoText2.text = "" + infoText.Substring(0, i) + "";
            if (i == 67)
                swipeIcon.SetActive(true);
            if (i == 120)
                swipeIcon.SetActive(false);
            yield return new WaitForSeconds(.08f);
        }
        yield return null;
    }

}
