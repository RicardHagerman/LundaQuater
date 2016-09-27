using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerBank : MonoBehaviour
{
    public static BeerBank Current;
    List<Challenge> challenges;
    List<GameObject> bankButtons;
    [SerializeField]
    GameObject bankButtonPrefab;
    [SerializeField]
    Transform bankButtonHolder;
    [SerializeField]
    GameObject drinkButton;
    [SerializeField]
    Text drinkButtontext;
    [SerializeField]
    Text instructionText;
    [SerializeField]
    Text winnerText;
    [SerializeField]
    Text loserText;
    [SerializeField]
    Text vsText;
    Challenge currentChallenge;

    void OnEnable()
    {
        if (Players.Current != null)
            challenges = Players.Current.GetChallenges();
        if (Current == null)
            Current = this;
        bankButtons = new List<GameObject>();
        drinkButton.SetActive(false);
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                instructionText.text = "ÖL BANK";
                break;
            case 1:
                instructionText.text = "BEER BANK";
                break;
            case 2:
                instructionText.text = "Gogtote popåsose";
                break;
        }
        loserText.text = "";
        winnerText.text = "";
        vsText.text = "";
        currentChallenge = null;
        MakeBank();
    }


    void OnDisable()
    {
        if (bankButtons.Count > 0)
        {
            foreach (var bb in bankButtons)
            {
                Destroy(bb);
            }
        }
        bankButtons.Clear();
    }

    void MakeBank()
    {
        if (challenges == null || challenges.Count == 0)
            return;
        foreach (var c in challenges)
        {
            var bb = Instantiate(bankButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            bb.transform.SetParent(bankButtonHolder);
            bb.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            bb.GetComponent<BeerBankButton>().Winner = c.winner;
            bb.GetComponent<BeerBankButton>().Loser = c.loser;
            bb.GetComponent<BeerBankButton>().Date = c.date;
            bb.GetComponent<BeerBankButton>().CH = c;
            bankButtons.Add(bb);
        }
    }

    public void Collect(string loser, string winner, Challenge c)
    {
        loserText.text = loser;
        winnerText.text = winner;
        currentChallenge = c;
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                vsText.text = "köper en öl åt";
                drinkButtontext.text = "Köp";
                break;
            case 1:
                vsText.text = "will buy a beer for";
                drinkButtontext.text = "Buy";
                break;
            case 2:
                vsText.text = "soskoka goge enon ölol totilol";
                drinkButtontext.text = "Goglolugog";
                break;
        }
        drinkButton.SetActive(true);
    }

    public void Drink()
    {
        if (bankButtons.Count > 0)
        {
            foreach (var bb in bankButtons)
            {
                Destroy(bb);
            }
        }
        bankButtons.Clear();
        challenges.Remove(currentChallenge);
        Players.Current.SaveChallenges(challenges);
        MakeBank();
        loserText.text = "";
        winnerText.text = "";
        vsText.text = "";
        drinkButton.SetActive(false);
    }


}
