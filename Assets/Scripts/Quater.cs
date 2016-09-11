using UnityEngine;

public class Quater : MonoBehaviour
{
    Rigidbody RB;
    public float x;
    int numBounce;
    bool miss;

    void OnEnable()
    {
        RB = GetComponent<Rigidbody>();
        QuaterBack.Hit += OnHit;
    }

    void OnDisable()
    {
        QuaterBack.Hit -= OnHit;
    }

    void OnHit()
    {
        miss = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Table")
        {
            numBounce++;
            var speed = RB.velocity.x;
            var middle = 0 + Mathf.Abs(transform.position.x);
            if (middle < 0.2f)
                RB.AddForce(new Vector3(-speed + x, 0, 0), ForceMode.Impulse);
            else if (transform.position.x > 0)
                RB.AddForce(new Vector3(-speed * 0.5f + x, 0, 0), ForceMode.Impulse);
            else if (transform.position.x < 0)
                RB.AddForce(new Vector3(-speed * 1.5f + x, 0, 0), ForceMode.Impulse);
            if (QuaterBack.FirstBounce != null && numBounce == 1)
            {
                QuaterBack.FirstBounce();
                return;
            }
            if (QuaterBack.Miss != null && miss == false)
            {
                QuaterBack.Miss();
                miss = true;
            }
        }
    }

}
