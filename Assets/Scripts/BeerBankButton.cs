using UnityEngine;
using UnityEngine.UI;

public class BeerBankButton : MonoBehaviour
{

    [SerializeField]
    Text winnerText;
    [SerializeField]
    Text loserText;
    [SerializeField]
    Text dateText;

    string _winner;
    string _loser;
    string _date;

    public string Winner { get { return _winner; } set { _winner = value; winnerText.text = _winner; } }
    public string Loser { get { return _loser; } set { _loser = value; loserText.text = _loser; } }
    public string Date { get { return _date; } set { _date = value; dateText.text = _date; } }

    public Challenge CH;

    public void CollectBeer()
    {
        BeerBank.Current.Collect(_loser, _winner, CH);
    }
}
