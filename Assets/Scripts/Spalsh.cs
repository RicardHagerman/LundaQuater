using UnityEngine;

public class Spalsh : MonoBehaviour
{
    AudioSource sound;
    bool hit;

    void OnEnable()
    {
        QuaterBack.NewRound += OnNewRound;
        sound = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        QuaterBack.NewRound -= OnNewRound;
    }

    void OnNewRound()
    {
        hit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hit)
            return;
        sound.Play();
        if (QuaterBack.Hit != null)
            QuaterBack.Hit();
        hit = true;
    }

}
