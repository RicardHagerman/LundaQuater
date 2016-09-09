using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour
{

    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Table");
        sound.Play();
    }

}
