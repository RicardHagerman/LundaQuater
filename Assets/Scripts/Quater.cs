using UnityEngine;

public class Quater : MonoBehaviour
{
    Rigidbody RB;
    public float x;
    bool firstBounce;

    void OnEnable()
    {
        RB = GetComponent<Rigidbody>();
        RB.maxAngularVelocity = 50f;
        RB.freezeRotation = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (firstBounce)
            return;
        firstBounce = true;
        var speed = RB.velocity;
        var middle = 0 + Mathf.Abs(transform.position.x);
        RB.AddForce(new Vector3(0, 0, 1f), ForceMode.Impulse);
        RB.freezeRotation = false;
        RB.AddTorque(10, 0, 0);
        if (middle < 0.2f)
        {
            RB.velocity = new Vector3(0, speed.y, speed.z);
        }
        else if (transform.position.x >= 0.2f)
        {
            RB.velocity = new Vector3(0.4f, speed.y, speed.z);
            RB.AddForce(new Vector3(x, 0, 0), ForceMode.Impulse);
        }
        else if (transform.position.x <= -0.2f)
        {
            RB.velocity = new Vector3(-0.4f, speed.y, speed.z);
            RB.AddForce(new Vector3(x, 0, 0), ForceMode.Impulse);
        }
        if (QuaterBack.FirstBounce != null)
            QuaterBack.FirstBounce();
    }
}
