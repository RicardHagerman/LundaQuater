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
    [SerializeField]
    GameObject info;

    Coroutine timer;
    int numHits;
    int numRounds;

    void Start()
    {
        info.SetActive(false);
        yay.text = "";
        score.text = "";
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
        score.text = "";
        yay.text = "";
        newGameButton.SetActive(false);
        foreach (var button in startGameButtons)
            button.SetActive(true);
    }

    public void Beginner()
    {
        Aim.Current.currentLevel = Level.BEGINNER;
        info.SetActive(true);
        StartNewGame();
    }

    public void Normal()
    {
        Aim.Current.currentLevel = Level.NORMAL;
        StartNewGame();
    }

    public void Hard()
    {
        Aim.Current.currentLevel = Level.HARD;
        StartNewGame();
    }

    void StartNewGame()
    {
        foreach (var button in startGameButtons)
            button.SetActive(false);
        if (QuaterBack.FirstQuater != null)
            QuaterBack.FirstQuater();
        if (QuaterBack.NewRound != null)
            QuaterBack.NewRound();
        score.text = "" + numHits + "  /  " + numRounds;
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
        if (info.activeInHierarchy)
            info.SetActive(false);
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
            yield return new WaitForSeconds(1.5f);
            yay.text = "";
            NewRound();
        }

    }



}
