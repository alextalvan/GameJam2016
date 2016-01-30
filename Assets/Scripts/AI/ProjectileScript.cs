using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{

    private Vector2 velocity;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = rb2d.velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManagerScript.Enabled)
        {
            if (rb2d.isKinematic)
            {
                rb2d.isKinematic = false;
                rb2d.velocity = velocity;
            }
        }
        else
        {
            if (!rb2d.isKinematic)
            {
                velocity = rb2d.velocity;
                rb2d.isKinematic = true;
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monk")
        {
            Debug.Log("HIT");
        }
        Destroy(gameObject);
    }
}
