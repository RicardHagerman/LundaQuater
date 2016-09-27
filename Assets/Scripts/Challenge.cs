using System;
using UnityEngine;

[Serializable]
public class Challenge
{
    public string date;
    public string winner;
    public string loser;

    public Challenge()
    {
        date = DateTime.Now.Month + "/" + DateTime.Now.Day + "  " + DateTime.Now.Year;
    }



}
