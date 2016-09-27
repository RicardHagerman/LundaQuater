using System;
using System.Collections.Generic;

public enum Level
{
    BEGINNER,
    NORMAL,
    HARD,
    SHOW
}

public static class QuaterBack
{
    public static Action FirstQuater;
    public static Action NewRound;
    public static Action NewMarker;
    public static Action FirstBounce;
    public static Action Hit;
    public static Action NewPlayer;

    public static bool PlayerChoosen;
    public static Player PlayerOne;
    public static Player PlayerTwo;
    public static int CurrentScore;

    public static bool EditiPlayer;
    public static bool Training;
    public static bool SinglePlayer;
    public static bool Challenge;




}