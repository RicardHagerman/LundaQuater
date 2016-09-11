using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject newGameButton;
    [SerializeField]
    GameObject[] startGameButtons;
    [SerializeField]
    Text yay;
    [SerializeField]
    Text score;

    Coroutine timer;
    int numHits;
    int numRounds;

    void Start()
    {
        yay.text = "";
        score.text = " 0  /  0 ";
        QuaterBack.Miss += OnMiss;
        QuaterBack.Hit += OnHit;
        Aim.Current.currentLevel = Level.SHOW;
        if (QuaterBack.FirstQuater != null)
            QuaterBack.FirstQuater();
        if (QuaterBack.NewRound != null)
            QuaterBack.NewRound();
        foreach (var button in startGameButtons)
            button.SetActive(false);
        newGameButton.SetActive(true);
    }

    void OnDisable()
    {
        QuaterBack.Miss -= OnMiss;
        QuaterBack.Hit -= OnHit;
    }

    void Update()
    {

    }

    void NewRound()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        if (QuaterBack.NewRound != null)
            QuaterBack.NewRound();
    }

    public void NewGame()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        numRounds = 0;
        numHits = 0;
        Aim.Current.currentLevel = Level.BEGINNER;
        if (QuaterBack.FirstQuater != null)
            QuaterBack.FirstQuater();
        if (QuaterBack.NewRound != null)
            QuaterBack.NewRound();
        score.text = "" + numHits + "  /  " + numRounds;
        yay.text = "";
        newGameButton.SetActive(false);
    }

    void OnMiss()
    {
        numRounds++;
        if (timer == null)
            timer = StartCoroutine(TimerCO());
        score.text = "" + numHits + "  /  " + numRounds;
        yay.text = "Oh no";
    }

    void OnHit()
    {
        numRounds++;
        numHits++;
        yay.text = "YAY";
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        timer = StartCoroutine(TimerCO());
        score.text = "" + numHits + "  /  " + numRounds;
    }


    IEnumerator TimerCO()
    {
        if (numRounds >= 3)
        {
            score.text = "" + numHits + "  /  " + numRounds;

            yield return new WaitForSeconds(1.5f);

            if (numHits > 2)
                yay.text = "SUPER";
            else
                yay.text = "No no no no no no no no no no";

            Aim.Current.currentLevel = Level.SHOW;
            newGameButton.SetActive(true);
            NewRound();
        }
        else
        {
            yield return new WaitForSeconds(2f);
            yay.text = "";
            NewRound();
        }

    }



}
