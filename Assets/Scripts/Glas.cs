using UnityEngine;

public class Glas : MonoBehaviour
{

    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("KLING");
        sound.Play();
    }

}
