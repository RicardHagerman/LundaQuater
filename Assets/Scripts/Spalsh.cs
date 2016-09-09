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
        Aim.Current.NewRound += OnNewRound;
    }

    void OnDisable()
    {
        Aim.Current.NewRound -= OnNewRound;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SPALSHHHH");
        sound.Play();
        yay.text = "YAY";
    }

    void OnNewRound()
    {
        yay.text = "";
    }



}
