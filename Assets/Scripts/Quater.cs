using UnityEngine;

public class Quater : MonoBehaviour
{
    Rigidbody RB;
    public float x;
    int numBounce;

    void OnEnable()
    {
        RB = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Table")
        {
            numBounce++;
            var speed = RB.velocity;
            var middle = 0 + Mathf.Abs(transform.position.x);
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

            if (QuaterBack.FirstBounce != null && numBounce == 1)
            {
                QuaterBack.FirstBounce();
                return;
            }

        }
    }

}
