using UnityEngine;

public class Quater : MonoBehaviour
{
    Rigidbody RB;
    public float x;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision collision)
    {
        var speed = RB.velocity.x;
        var middle = 0 + Mathf.Abs(transform.position.x);
        Debug.Log("QUATER " + x);
        if (middle < 0.2f)
            RB.AddForce(new Vector3(-speed + x, 0, 0), ForceMode.Impulse);
        else if (transform.position.x > 0)
            RB.AddForce(new Vector3(-speed * 0.5f + x, 0, 0), ForceMode.Impulse);
        else if (transform.position.x < 0)
            RB.AddForce(new Vector3(-speed * 1.5f + x, 0, 0), ForceMode.Impulse);
    }



}
