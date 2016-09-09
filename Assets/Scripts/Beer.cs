using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.renderQueue = 3001;
    }



}
