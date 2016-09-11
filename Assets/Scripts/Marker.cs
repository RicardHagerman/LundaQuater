using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{


    void OnEnable()
    {
        QuaterBack.FirstBounce += OnFirstBounce;
        QuaterBack.NewMarker += OnNewMarker;
    }

    void OnDisable()
    {
        QuaterBack.FirstBounce -= OnFirstBounce;
        QuaterBack.NewRound -= OnNewMarker;
    }

    void OnNewMarker()
    {
        var qp = Aim.Current.quaterPosition;
        transform.position = new Vector3(qp.x, 0.01f, qp.z);
    }

    void OnFirstBounce()
    {
        transform.position = new Vector3(0, -1, 0);
    }



}
