using System;

[Serializable]
public class Player
{

    string name;
    int score;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

}
