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

	void Start ()
	{
		newGameButton.SetActive (true);
		SetLanguage ();
		info.SetActive (false);
		yay.text = "";
		score.text = "";
		QuaterBack.FirstBounce += OnFirstBounce;
		QuaterBack.Hit += OnHit;
		Aim.Current.currentLevel = Level.SHOW;
		if (QuaterBack.FirstQuater != null)
			QuaterBack.FirstQuater ();
		if (QuaterBack.NewRound != null)
			QuaterBack.NewRound ();
		foreach (var button in startGameButtons)
			button.SetActive (false);
		menuPanel.SetActive (false);
	}

	void OnDisable ()
	{
		QuaterBack.FirstBounce -= OnFirstBounce;
		QuaterBack.Hit -= OnHit;
	}

	public void ResetPlayerPrefs ()
	{
		PlayerPrefs.DeleteAll ();
	}

	public void ShowMenu ()
	{
		menuPanel.SetActive (true);
		info.SetActive (false);
	}

	public void Swedish ()
	{
		PlayerPrefs.SetInt ("Language", 0);
		SetLanguage ();
	}

	public void English ()
	{
		PlayerPrefs.SetInt ("Language", 1);
		SetLanguage ();
	}

	public void Nonofof ()
	{
		PlayerPrefs.SetInt ("Language", 2);
		SetLanguage ();
	}

	public void NewGame ()
	{
		menuPanel.SetActive (false);
		if (timer != null)
		{
			StopCoroutine (timer);
			timer = null;
		}
		numRounds = 0;
		numHits = 0;
		Aim.Current.currentLevel = Level.BEGINNER;
		score.text = "";
		yay.text = "";
		newGameButton.SetActive (false);
		foreach (var button in startGameButtons)
			button.SetActive (true);
	}

	public void Beginner ()
	{
		Aim.Current.currentLevel = Level.BEGINNER;
		roundsToPlay = 3;
		hitsToWin = 1;
		info.SetActive (true);
		StartNewGame ();
	}

	public void Normal ()
	{
		roundsToPlay = 3;
		hitsToWin = 2;
		Aim.Current.currentLevel = Level.NORMAL;
		StartNewGame ();
	}

	public void Hard ()
	{
		roundsToPlay = 5;
		hitsToWin = 4;
		Aim.Current.currentLevel = Level.HARD;
		StartNewGame ();
	}

	void StartNewGame ()
	{
		foreach (var button in startGameButtons)
			button.SetActive (false);
		if (QuaterBack.FirstQuater != null)
			QuaterBack.FirstQuater ();
		if (QuaterBack.NewRound != null)
			QuaterBack.NewRound ();
		score.text = "" + numHits + "  /  " + numRounds;
	}

	void OnFirstBounce ()
	{
		if (info.activeInHierarchy)
			info.SetActive (false);
		numRounds++;
		hit = false;
		if (timer != null)
		{
			StopCoroutine (timer);
			timer = null;
		}
		timer = StartCoroutine (TimerCO ());
		score.text = "" + numHits + "  /  " + numRounds;
	}

	void OnHit ()
	{
		numHits++;
		switch (PlayerPrefs.GetInt ("Language"))
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
			StopCoroutine (timer);
			timer = null;
		}
		timer = StartCoroutine (TimerCO ());
		score.text = "" + numHits + "  /  " + numRounds;
	}

	IEnumerator TimerCO ()
	{
		if (hit)
		{
			yield return new WaitForSeconds (1.5f);
			yay.text = "";
			if (numRounds >= roundsToPlay)
				Yay ();
			else
				NewRound ();
		}
		else
		{
			yield return new WaitForSeconds (3f);
			switch (PlayerPrefs.GetInt ("Language"))
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
			yield return new WaitForSeconds (1.5f);
			yay.text = "";
			if (numRounds >= roundsToPlay)
				Yay ();
			else
				NewRound ();
		}
	}

	void Yay ()
	{
		score.text = "" + numHits + "  /  " + numRounds;
		if (numHits >= hitsToWin)
			switch (PlayerPrefs.GetInt ("Language"))
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
		else
			switch (PlayerPrefs.GetInt ("Language"))
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
		Aim.Current.currentLevel = Level.SHOW;
		newGameButton.SetActive (true);
		NewRound ();
	}

	void NewRound ()
	{
		if (timer != null)
		{
			StopCoroutine (timer);
			timer = null;
		}
		if (QuaterBack.NewRound != null)
			QuaterBack.NewRound ();
	}



	void SetLanguage ()
	{
		switch (PlayerPrefs.GetInt ("Language"))
		{
		case 0:
			sweButtonText.color = Color.green;
			engButtonText.color = Color.white;
			nonfofButtonText.color = Color.white;
			newGameButtonText.text = "Spela";
			menuNewGameButtonText.text = "Mer öl";
			beginnerButtonText.text = "Lätt";
			normalButtonText.text = "Spela";
			hardButtonText.text = "Stor Stark";
			break;
		case 1:
			sweButtonText.color = Color.white;
			engButtonText.color = Color.green;
			nonfofButtonText.color = Color.white;
			newGameButtonText.text = "Play";
			menuNewGameButtonText.text = "Play";
			beginnerButtonText.text = "Beginner";
			normalButtonText.text = "Normal";
			hardButtonText.text = "Hard";
			break;
		case 2:
			sweButtonText.color = Color.white;
			engButtonText.color = Color.white;
			nonfofButtonText.color = Color.green;
			newGameButtonText.text = "Momeror";
			menuNewGameButtonText.text = "Nonu";
			beginnerButtonText.text = "Lolätotot";
			normalButtonText.text = "Ölol";
			hardButtonText.text = "rorajojror";
			break;
		}


	}
}
