using UnityEngine.UI;
using UnityEngine;


public class PlayerButton : MonoBehaviour
{

    [SerializeField]
    Text playerNameText;
    [SerializeField]
    Text playerScore;
    Player player;
    public Player Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
            playerNameText.text = player.Name;
            playerScore.text = "" + player.Score;
        }
    }

    public void ChoosenPlayer()
    {
        if (QuaterBack.PlayerChoosen)
            QuaterBack.PlayerTwo = player;
        else
            QuaterBack.PlayerOne = player;
        if (QuaterBack.NewPlayer != null)
            QuaterBack.NewPlayer();
    }

}
