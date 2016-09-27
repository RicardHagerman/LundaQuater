using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    public static Players Current;
    List<Player> _players;
    Serializer serializer;
    [SerializeField]
    GameObject playerButtonPrefab;
    List<GameObject> playerButtons;
    [SerializeField]
    Transform playerButtonHolder;
    [SerializeField]
    RectTransform scrolView;
    [SerializeField]
    InputField newPlayer;
    [SerializeField]
    GameObject Input;
    [SerializeField]
    Text instructions;
    int instructionsKey;
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    Text playButtonText;
    [SerializeField]
    GameObject newPlayerButton;
    [SerializeField]
    Text newPlayerButtonText;
    [SerializeField]
    Text p1;
    [SerializeField]
    Text p2;
    [SerializeField]
    Text vs;
    [SerializeField]
    GameObject deleteScoreButton;
    [SerializeField]
    Text deleteScoreButtonText;
    [SerializeField]
    GameObject deletePlayerButton;
    [SerializeField]
    Text deletePlayerButtonText;
    [SerializeField]
    GameObject menuButton;
    [SerializeField]
    GameObject menu;
    Player playerToEdit;
    bool firstPlayer;

    void Awake()
    {
        serializer = new Serializer();
        Current = this;
    }

    void Start()
    {
        QuaterBack.NewPlayer += OnNewPlayer;
        playerButtons = new List<GameObject>();
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        QuaterBack.EditiPlayer = false;
        firstPlayer = false;
    }

    public void ShowPlayerPanel()
    {
        QuaterBack.PlayerChoosen = false;
        playerToEdit = null;
        instructionsKey = 0;
        if (QuaterBack.SinglePlayer)
        {
            QuaterBack.PlayerChoosen = true;
            instructionsKey = 1;
        }
        SetButtonsText();
        newPlayerButton.SetActive(true);
        playButton.SetActive(false);
        Input.SetActive(true);
        deleteScoreButton.SetActive(false);
        deletePlayerButton.SetActive(false);
        menuButton.SetActive(false);
        newPlayer.text = "";
        _players = serializer.getSavedPlayers("players");
        p1.text = "";
        p2.text = "";
        vs.text = "";
        if (QuaterBack.EditiPlayer)
        {
            newPlayerButton.SetActive(false);
            Input.SetActive(false);
            menuButton.SetActive(true);
            QuaterBack.PlayerChoosen = true;
            instructionsKey = 3;
            if (_players.Count == 0)
            {
                newPlayerButton.SetActive(true);
                Input.SetActive(true);
                instructionsKey = 4;
                firstPlayer = true;
            }
        }
        MakePlayerButtons();
        WriteInstructions();
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void OnDisable()
    {
        QuaterBack.NewPlayer -= OnNewPlayer;
        if (playerButtons.Count > 0)
            foreach (var button in playerButtons)
                Destroy(button);
        QuaterBack.EditiPlayer = false;
        firstPlayer = false;
    }

    void MakePlayerButtons()
    {
        int numButtons = 0;
        playerButtonHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        GameObject playerButton;
        if (playerButtons.Count > 0)
            foreach (var button in playerButtons)
                Destroy(button);
        playerButtons.Clear();
        SortPlayers();
        foreach (var player in _players)
        {
            playerButton = Instantiate(playerButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            playerButton.transform.SetParent(playerButtonHolder);
            playerButton.GetComponent<PlayerButton>().Player = player;
            playerButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            playerButtons.Add(playerButton);
            numButtons++;
        }
    }

    public void NewPlayer()
    {
        if (newPlayer.text == "")
            return;
        var pn = newPlayer.text;
        newPlayer.text = "";
        var p = new Player();
        p.Name = pn;
        _players.Add(p);
        MakePlayerButtons();
        serializer.savePlayers(_players, "players");
        if (QuaterBack.PlayerChoosen)
            QuaterBack.PlayerTwo = p;
        else
            QuaterBack.PlayerOne = p;
        if (QuaterBack.NewPlayer != null)
            QuaterBack.NewPlayer();
    }

    void OnNewPlayer()
    {
        if (QuaterBack.EditiPlayer)
        {
            playerToEdit = QuaterBack.PlayerTwo;
            EditPlayer();
            return;
        }
        QuaterBack.PlayerChoosen = true;
        if (instructionsKey == 0)
        {
            instructionsKey = 1;
            p1.text = QuaterBack.PlayerOne.Name;
            vs.text = "vs";
            WriteInstructions();
        }
        else
        {
            instructionsKey = 2;
            p2.text = QuaterBack.PlayerTwo.Name;
            WriteInstructions();
            newPlayerButton.SetActive(false);
            playButton.SetActive(true);
            Input.SetActive(false);
        }
    }

    void WriteInstructions()
    {
        switch (instructionsKey)
        {
            case 0:
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        instructions.text = "Välj en spelare eller skapa en ny";
                        break;
                    case 1:
                        instructions.text = "Choose a player or create a new one";
                        break;
                    case 2:
                        instructions.text = "Totvovå Kokilolo sosocockokeror .....";
                        break;
                }
                break;
            case 1:
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        instructions.text = "Välj en spelare eller skapa en ny";
                        break;
                    case 1:
                        instructions.text = "Choose a player or create a new one";
                        break;
                    case 2:
                        instructions.text = "Totvovå Kokilolo sosocockokeror .....";
                        break;
                }
                break;
            case 2:
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        instructions.text = "Tryck på - Spela -";
                        break;
                    case 1:
                        instructions.text = "Press Play";
                        break;
                    case 2:
                        instructions.text = "å momasosoror momedod ölol ...";
                        break;
                }
                break;
            case 3:
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        instructions.text = "Välj en spelare att redigera";
                        break;
                    case 1:
                        instructions.text = "Choose a player to edit";
                        break;
                    case 2:
                        instructions.text = "Tota boboror enon sospopelolarore";
                        break;
                }
                break;
            case 4:
                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        instructions.text = "Skapa en ny spelare";
                        break;
                    case 1:
                        instructions.text = "Create a new player";
                        break;
                    case 2:
                        instructions.text = "Gogöror enon sospopelolarore";
                        break;
                }
                break;
        }
    }

    public void ClosePlayerPanel()
    {
        if (playerButtons.Count > 0)
            foreach (var button in playerButtons)
                Destroy(button);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        QuaterBack.EditiPlayer = false;
        firstPlayer = false;
    }

    public void SavePlayers()
    {
        serializer.savePlayers(_players, "players");
    }

    public void SaveChallenges(List<Challenge> data)
    {
        serializer.saveChallenges(data, "challenges");
    }

    public List<Challenge> GetChallenges()
    {
        var temp = serializer.getSavedChallenges("challenges");
        if (temp == null)
            temp = new List<Challenge>();
        return temp;
    }

    void SortPlayers()
    {

        if (_players.Count == 0)
            return;

        for (int i = 0; i < _players.Count - 1; i++)
        {
            for (int j = _players.Count - 1; j > i; j--)
            {
                if (_players[j].Score > _players[j - 1].Score)
                {
                    Player temp = _players[j];
                    _players[j] = _players[j - 1];
                    _players[j - 1] = temp;
                }
            }
        }
    }

    void EditPlayer()
    {
        if (firstPlayer)
        {
            ToMenu();
            return;
        }
        p1.text = playerToEdit.Name;
        deleteScoreButton.SetActive(true);
        deletePlayerButton.SetActive(true);
    }

    public void ToMenu()
    {
        menu.SetActive(true);
        ClosePlayerPanel();
    }

    public void DeleteScore()
    {
        if (playerToEdit == null)
            return;
        playerToEdit.Score = 0;
        SavePlayers();
        MakePlayerButtons();
    }

    public void DeletePlayer()
    {
        if (playerToEdit == null)
            return;
        p1.text = "";
        _players.Remove(playerToEdit);
        SavePlayers();
        MakePlayerButtons();
    }

    void SetButtonsText()
    {
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                newPlayerButtonText.text = "Ny spelare";
                playButtonText.text = "Spela";
                deleteScoreButtonText.text = "Ta bort poäng";
                deletePlayerButtonText.text = "Ta bort spelare";
                break;
            case 1:
                newPlayerButtonText.text = "New player";
                playButtonText.text = "Play";
                deleteScoreButtonText.text = "Delete score";
                deletePlayerButtonText.text = "Delete player";
                break;
            case 2:
                newPlayerButtonText.text = "Nony";
                playButtonText.text = "Sospoplola";
                deleteScoreButtonText.text = "non popoänongog";
                deletePlayerButtonText.text = "dodödoda";
                break;
        }
    }


}
