using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject newGameButton;
    [SerializeField]
    Text newGameButtonText;
    [SerializeField]
    GameObject menuNewGameButton;
    [SerializeField]
    Text menuNewGameButtonText;
    [SerializeField]
    GameObject beginnerButton;
    [SerializeField]
    Text beginnerButtonText;
    [SerializeField]
    GameObject normalButton;
    [SerializeField]
    Text normalButtonText;
    [SerializeField]
    GameObject hardButton;
    [SerializeField]
    Text hardButtonText;
    [SerializeField]
    GameObject[] startGameButtons;
    [SerializeField]
    Text sweButtonText;
    [SerializeField]
    Text engButtonText;
    [SerializeField]
    Text nonfofButtonText;
    [SerializeField]
    Text EditPlayerButtonText;

    [SerializeField]
    Text yay;
    [SerializeField]
    Text score;
    [SerializeField]
    GameObject info;
    [SerializeField]
    Text scoreToBeat;
    [SerializeField]
    Text scoreToBeatPoints;

    [SerializeField]
    GameObject beerBankPanel;
    [SerializeField]
    Text beerBankButtonText;

    Coroutine timer;
    int numHits;
    int numRounds;
    bool hit;
    int roundsToPlay;
    int hitsToWin;

    Player currentPlayer;
    bool challengeRoundOne;
    int playerOneScore;
    List<Challenge> challenges;

    void Start()
    {
        newGameButton.SetActive(true);
        SetLanguage();
        info.SetActive(false);
        yay.text = "";
        score.text = "";
        scoreToBeat.text = "";
        scoreToBeat.color = Color.white;
        scoreToBeatPoints.text = "";
        QuaterBack.FirstBounce += OnFirstBounce;
        QuaterBack.Hit += OnHit;
        Aim.Current.currentLevel = Level.SHOW;
        if (QuaterBack.FirstQuater != null)
            QuaterBack.FirstQuater();
        if (QuaterBack.NewRound != null)
            QuaterBack.NewRound();
        foreach (var button in startGameButtons)
            button.SetActive(false);
        menuPanel.SetActive(false);
        challenges = Players.Current.GetChallenges();
    }

    void OnDisable()
    {
        QuaterBack.FirstBounce -= OnFirstBounce;
        QuaterBack.Hit -= OnHit;
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OpenPlayersPanel()
    {
        Players.Current.ShowPlayerPanel();
        NewGame();
        foreach (var button in startGameButtons)
            button.SetActive(false);
    }

    public void ShowMenu()
    {
        newGameButton.SetActive(false);
        menuPanel.SetActive(true);
        Players.Current.ClosePlayerPanel();
        beerBankPanel.SetActive(false);
        info.SetActive(false);
    }

    public void Swedish()
    {
        PlayerPrefs.SetInt("Language", 0);
        SetLanguage();
    }

    public void English()
    {
        PlayerPrefs.SetInt("Language", 1);
        SetLanguage();
    }

    public void Nonofof()
    {
        PlayerPrefs.SetInt("Language", 2);
        SetLanguage();
    }

    public void NewGame()
    {
        menuPanel.SetActive(false);
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
        scoreToBeat.text = "";
        scoreToBeat.color = Color.white;
        scoreToBeatPoints.text = "";
        newGameButton.SetActive(false);
        foreach (var button in startGameButtons)
            button.SetActive(true);
        challenges = Players.Current.GetChallenges();
    }

    public void Beginner()
    {
        Aim.Current.currentLevel = Level.BEGINNER;
        currentPlayer = null;
        QuaterBack.Training = true;
        QuaterBack.SinglePlayer = false;
        QuaterBack.Challenge = false;
        roundsToPlay = 3;
        challengeRoundOne = false;
        info.SetActive(true);
        StartNewGame();
    }

    public void Singel()
    {
        roundsToPlay = 1;
        challengeRoundOne = false;
        Aim.Current.currentLevel = Level.NORMAL;
        QuaterBack.Training = false;
        QuaterBack.SinglePlayer = true;
        QuaterBack.Challenge = false;
        Players.Current.ShowPlayerPanel();
        foreach (var button in startGameButtons)
            button.SetActive(false);
    }

    public void Challenge()
    {
        roundsToPlay = 1;
        challengeRoundOne = true;
        playerOneScore = 0;
        Aim.Current.currentLevel = Level.NORMAL;
        QuaterBack.Training = false;
        QuaterBack.SinglePlayer = false;
        QuaterBack.Challenge = true;
        Players.Current.ShowPlayerPanel();
        foreach (var button in startGameButtons)
            button.SetActive(false);
    }

    public void StartNewGame()
    {
        foreach (var button in startGameButtons)
            button.SetActive(false);
        if (QuaterBack.FirstQuater != null)
            QuaterBack.FirstQuater();
        if (QuaterBack.NewRound != null)
            QuaterBack.NewRound();
        if (QuaterBack.SinglePlayer)
        {
            score.text = "" + QuaterBack.PlayerTwo.Name + "  :  " + numHits;
            currentPlayer = QuaterBack.PlayerTwo;
            hitsToWin = currentPlayer.Score;
            WriteScoreToBeat();
        }
        else if (QuaterBack.Training)
        {
            score.text = "" + numHits + " / " + roundsToPlay;
        }
        else if (QuaterBack.Challenge)
        {
            score.text = "" + QuaterBack.PlayerOne.Name + "  :  " + numHits;
            currentPlayer = QuaterBack.PlayerOne;
            hitsToWin = QuaterBack.PlayerTwo.Score;
            WriteScoreToBeat();
        }
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
        if (QuaterBack.SinglePlayer)
            score.text = "" + QuaterBack.PlayerTwo.Name + "  :  " + numHits;
        else if (QuaterBack.Training)
        {
            score.text = "" + numHits + " / " + roundsToPlay;
        }
    }

    void OnHit()
    {
        numHits++;
        if (QuaterBack.SinglePlayer || QuaterBack.Challenge)
        {
            roundsToPlay++;
            if (numHits > 4)
            {
                Aim.Current.currentLevel = Level.HARD;
            }
        }
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                yay.text = "YAY";
                break;
            case 1:
                yay.text = "YAY";
                break;
            case 2:
                yay.text = "Boburorpop";
                break;
        }
        hit = true;
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        timer = StartCoroutine(TimerCO());
        if (QuaterBack.SinglePlayer || QuaterBack.Challenge)
        {
            if (challengeRoundOne)
                score.text = "" + QuaterBack.PlayerOne.Name + "  :  " + numHits;
            else
                score.text = "" + QuaterBack.PlayerTwo.Name + "  :  " + numHits;
        }
        else if (QuaterBack.Training)
        {
            score.text = "" + numHits + " / " + roundsToPlay;
        }
    }

    IEnumerator TimerCO()
    {
        if (hit)
        {
            if (currentPlayer != null)
            {
                if (numHits > hitsToWin)
                {
                    scoreToBeat.color = Color.green;
                    scoreToBeatPoints.text = "" + numHits;
                }
            }
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
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    yay.text = "Missssss......";
                    break;
                case 1:
                    yay.text = "Oh no";
                    break;
                case 2:
                    yay.text = "soskokitot åsoså";
                    break;
            }
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
        if (QuaterBack.SinglePlayer || QuaterBack.Challenge)
        {
            if (challengeRoundOne)
                score.text = "" + QuaterBack.PlayerOne.Name + "  :  " + numHits;
            else
                score.text = "" + QuaterBack.PlayerTwo.Name + "  :  " + numHits;
        }
        else if (QuaterBack.Training)
        {
            score.text = "" + numHits + " / " + roundsToPlay;
        }
        int great = 1;
        if (currentPlayer != null)
            great = currentPlayer.Score;
        if (numHits > great)
        {
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    yay.text = "Jag är bäst";
                    break;
                case 1:
                    yay.text = "I'm invincible";
                    break;
                case 2:
                    yay.text = "Momeror ölol nonu";
                    break;
            }
            if (QuaterBack.SinglePlayer || QuaterBack.Challenge)
            {
                currentPlayer.Score = numHits;
                Players.Current.SavePlayers();
            }
        }
        else
        {
            if (currentPlayer != null)
            {
                if (numHits < hitsToWin)
                {
                    scoreToBeat.color = Color.red;
                }
            }
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    yay.text = "F . . ";
                    break;
                case 1:
                    yay.text = "No Cigar";
                    break;
                case 2:
                    yay.text = "%&####%&/(&%(&%/ fof";
                    break;
            }
        }
        if (QuaterBack.Challenge && challengeRoundOne)
        {
            if (numHits > currentPlayer.Score)
                currentPlayer.Score = numHits;
            Players.Current.SavePlayers();
            Aim.Current.currentLevel = Level.NORMAL;
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    yay.text = "Nästa";
                    break;
                case 1:
                    yay.text = "Next";
                    break;
                case 2:
                    yay.text = "Dodinon toturor";
                    break;
            }
            roundsToPlay = 1;
            numRounds = 0;
            hitsToWin = numHits;
            playerOneScore = numHits;
            numHits = 0;
            score.text = "" + QuaterBack.PlayerTwo.Name + "  :  " + numHits;
            currentPlayer = QuaterBack.PlayerTwo;
            challengeRoundOne = false;
            scoreToBeat.color = Color.white;
            WriteScoreToBeat();
            NewRound();
        }
        else if (QuaterBack.Challenge)
        {
            if (numHits > currentPlayer.Score)
                currentPlayer.Score = numHits;
            Players.Current.SavePlayers();
            string winner = "";
            string wText = "";
            if (playerOneScore > numHits)
            {
                winner = QuaterBack.PlayerOne.Name;
                var c = new Challenge();
                c.winner = winner;
                c.loser = QuaterBack.PlayerTwo.Name;
                challenges.Add(c);
                Players.Current.SaveChallenges(challenges);
            }
            else if (playerOneScore < numHits)
            {
                winner = QuaterBack.PlayerTwo.Name;
                var c = new Challenge();
                c.winner = winner;
                c.loser = QuaterBack.PlayerOne.Name;
                challenges.Add(c);
                Players.Current.SaveChallenges(challenges);
            }
            else
            {
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        winner = "Ingen";
                        break;
                    case 1:
                        winner = "None";
                        break;
                    case 2:
                        winner = "???????????";
                        break;
                }
            }
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    wText = "VINNARE !!!!";
                    break;
                case 1:
                    wText = "WINNER !!!!";
                    break;
                case 2:
                    wText = "Kokinongog";
                    break;
            }
            yay.text = wText + "   " + winner;
            score.text = "" + QuaterBack.PlayerOne.Name + " : " + playerOneScore + "\n" + QuaterBack.PlayerTwo.Name + " : " + numHits;
            scoreToBeat.text = "";
            scoreToBeat.color = Color.white;
            scoreToBeatPoints.text = "";
            playerOneScore = 0;
            numHits = 0;
            Aim.Current.currentLevel = Level.SHOW;
            newGameButton.SetActive(true);
            NewRound();
        }
        else
        {
            Aim.Current.currentLevel = Level.SHOW;
            newGameButton.SetActive(true);
            NewRound();
        }
        //if (challenges.Count > 0)
        //{
        //    foreach (var c in challenges)
        //    {
        //        Debug.Log("" + c.date + " " + c.winner + " " + c.loser);
        //    }
        //}
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



    void SetLanguage()
    {
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                sweButtonText.color = Color.green;
                engButtonText.color = Color.white;
                nonfofButtonText.color = Color.white;
                newGameButtonText.text = "Spela";
                menuNewGameButtonText.text = "Spela";
                beginnerButtonText.text = "Lätt";
                normalButtonText.text = "En Spelare";
                hardButtonText.text = "Utmana";
                EditPlayerButtonText.text = "Spelare";
                beerBankButtonText.text = "Öl Bank";
                break;
            case 1:
                sweButtonText.color = Color.white;
                engButtonText.color = Color.green;
                nonfofButtonText.color = Color.white;
                newGameButtonText.text = "Play";
                menuNewGameButtonText.text = "Play";
                beginnerButtonText.text = "Beginner";
                normalButtonText.text = "Singel";
                hardButtonText.text = "Challenge";
                EditPlayerButtonText.text = "Edit Player";
                beerBankButtonText.text = "Beer Bank";
                break;
            case 2:
                sweButtonText.color = Color.white;
                engButtonText.color = Color.white;
                nonfofButtonText.color = Color.green;
                newGameButtonText.text = "Momeror";
                menuNewGameButtonText.text = "Nonu";
                beginnerButtonText.text = "Lolätotot";
                normalButtonText.text = "Jojagog";
                hardButtonText.text = "Tungur Knivur";
                EditPlayerButtonText.text = "momomomo";
                beerBankButtonText.text = "SEB";
                break;
        }
    }

    void WriteScoreToBeat()
    {
        scoreToBeatPoints.text = "" + hitsToWin;
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                scoreToBeat.text = "rekord";
                break;
            case 1:
                scoreToBeat.text = "High Score";
                break;
            case 2:
                scoreToBeat.text = "Momålol";
                break;
        }
    }

    public void EditPlayer()
    {
        QuaterBack.EditiPlayer = true;
        Players.Current.ShowPlayerPanel();
        NewGame();
        foreach (var button in startGameButtons)
            button.SetActive(false);
    }

    public void OpenBeerBank()
    {
        beerBankPanel.SetActive(true);
        menuPanel.SetActive(false);
        NewGame();
        foreach (var button in startGameButtons)
            button.SetActive(false);
    }

}
