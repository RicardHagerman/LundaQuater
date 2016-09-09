using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quater : MonoBehaviour
{
    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("QUA");

        if (transform.position.x > 0.15f)
            RB.AddForce(new Vector3(.5f, 0, 0), ForceMode.Impulse);
        else if (transform.position.x < -0.15f)
            RB.AddForce(new Vector3(-2, 0, 0), ForceMode.Impulse);
        else
            RB.AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);

    }



}
