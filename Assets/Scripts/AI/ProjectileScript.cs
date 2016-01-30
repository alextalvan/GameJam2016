using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    MonkManager monkmng;

    [SerializeField]
    private int chargeDamage = 10;

    [SerializeField]
    private int attackDamage = 10;

    private Vector2 velocity;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        monkmng = GameObject.Find("Monks").GetComponent<MonkManager>();
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
            //Debug.Log("HIT");

            Monk m = coll.collider.GetComponent<Monk>();
            m.Stun();
            m.Damage(attackDamage);

            monkmng.RemoveCharge(chargeDamage);

        }
        Destroy(gameObject);
    }
}
