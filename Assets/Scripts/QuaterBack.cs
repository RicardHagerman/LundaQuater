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
}
