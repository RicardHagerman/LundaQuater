using UnityEngine;

public class Miss : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (QuaterBack.Miss != null)
            QuaterBack.Miss();
    }



}
