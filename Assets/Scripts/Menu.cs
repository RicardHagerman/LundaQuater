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
    bool hit;
    int roundsToPlay;
    int hitsToWin;

    void Start()
    {
        info.SetActive(false);
        yay.text = "";
        score.text = "";
        QuaterBack.FirstBounce += OnFirstBounce;
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
        QuaterBack.FirstBounce -= OnFirstBounce;
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
        roundsToPlay = 3;
        hitsToWin = 1;
        info.SetActive(true);
        StartNewGame();
    }

    public void Normal()
    {
        roundsToPlay = 3;
        hitsToWin = 2;
        Aim.Current.currentLevel = Level.NORMAL;
        StartNewGame();
    }

    public void Hard()
    {
        roundsToPlay = 5;
        hitsToWin = 4;
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

    void OnFirstBounce()
    {
        if (info.activeInHierarchy)
            info.SetActive(false);
        numRounds++;
        hit = false;
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        timer = StartCoroutine(TimerCO());
        score.text = "" + numHits + "  /  " + numRounds;
    }

    void OnHit()
    {
        numHits++;
        yay.text = "YAY";
        hit = true;
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
        if (hit)
        {
            yield return new WaitForSeconds(1.5f);
            yay.text = "";
            if (numRounds >= roundsToPlay)
                Yay();
            else
                NewRound();
        }
        else
        {
            yield return new WaitForSeconds(3f);
            yay.text = "Oh No no";
            yield return new WaitForSeconds(1.5f);
            yay.text = "";
            if (numRounds >= roundsToPlay)
                Yay();
            else
                NewRound();
        }
    }

    void Yay()
    {
        score.text = "" + numHits + "  /  " + numRounds;
        if (numHits >= hitsToWin)
            yay.text = "SUPER";
        else
            yay.text = "No no no no no no no no no no";
        Aim.Current.currentLevel = Level.SHOW;
        newGameButton.SetActive(true);
        NewRound();
    }



}
