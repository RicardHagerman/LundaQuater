using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Serializer
{


    //public Serializer()
    //{
    //    if (Application.platform == RuntimePlatform.IPhonePlayer)
    //        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
    //}


    public List<Player> getSavedPlayers(string name)
    {
        List<Player> tempList;
        string data = PlayerPrefs.GetString(name);
        if (!string.IsNullOrEmpty(data))
        {
            try
            {
                using (MemoryStream m = new MemoryStream(Convert.FromBase64String(data)))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    tempList = (List<Player>)b.Deserialize(m);
                    return tempList;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex + "WTF ------ getSavedPlayers");
                tempList = new List<Player>();
                return tempList;
            }
        }
        else
        {
            tempList = new List<Player>();
            return tempList;
        }
    }

    public void savePlayers(List<Player> Data, string name)
    {
        BinaryFormatter b = new BinaryFormatter();
        MemoryStream m = new MemoryStream();
        b.Serialize(m, Data);
        PlayerPrefs.SetString(name, Convert.ToBase64String(m.GetBuffer()));
    }


    public List<Challenge> getSavedChallenges(string name)
    {
        List<Challenge> tempList;
        string data = PlayerPrefs.GetString(name);
        if (!string.IsNullOrEmpty(data))
        {
            try
            {
                using (MemoryStream m = new MemoryStream(Convert.FromBase64String(data)))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    tempList = (List<Challenge>)b.Deserialize(m);
                    return tempList;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex + "WTF ------ getSavedPlayers");
                tempList = new List<Challenge>();
                return tempList;
            }
        }
        else
        {
            tempList = new List<Challenge>();
            return tempList;
        }
    }

    public void saveChallenges(List<Challenge> Data, string name)
    {
        BinaryFormatter b = new BinaryFormatter();
        MemoryStream m = new MemoryStream();
        b.Serialize(m, Data);
        PlayerPrefs.SetString(name, Convert.ToBase64String(m.GetBuffer()));
    }



    public void Clear(string name)
    {
        PlayerPrefs.DeleteAll();
    }



}
