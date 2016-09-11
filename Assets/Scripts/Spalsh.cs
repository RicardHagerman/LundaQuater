using UnityEngine;
using UnityEngine.UI;

public class Spalsh : MonoBehaviour
{
    [SerializeField]
    Text yay;

    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        QuaterBack.NewRound += OnNewRound;
    }

    void OnDisable()
    {
        QuaterBack.NewRound -= OnNewRound;
    }

    void OnTriggerEnter(Collider other)
    {
        sound.Play();
        yay.text = "YAY";
    }

    void OnNewRound()
    {
        yay.text = "";
    }



}
