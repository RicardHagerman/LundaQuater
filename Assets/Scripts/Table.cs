using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour
{

    AudioSource sound;

    bool makeSound;


    void OnEnable()
    {
        QuaterBack.NewRound += OnNewRound;
        QuaterBack.Hit += OnHit;
        sound = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        QuaterBack.NewRound -= OnNewRound;
        QuaterBack.Hit -= OnHit;
    }

    void OnNewRound()
    {
        makeSound = true;
    }

    void OnHit()
    {
        makeSound = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (makeSound)
            sound.Play();
    }

}
