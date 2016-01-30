using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private float attackDamage = 1f;
    [SerializeField]
    private float attackCoolDown = 1f;
    private float attackTimer;

    private Transform monks;
    private Transform targetMonk;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        monks = GameObject.Find("Monks").transform;
        GetClosestMonk();
        ResetAttackTimer();
    }

    void GetClosestMonk()
    {
        float closestDist = Mathf.Infinity;

        foreach (Transform monk in monks)
        {
            float dist = Vector3.Distance(monk.position, transform.position);
            if (dist < closestDist)
            {
                targetMonk = monk;
                closestDist = dist;
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateAttackTimer();
        Chase();
    }

    void Chase()
    {
        Vector2 vel = (targetMonk.position - transform.position).normalized * movementSpeed;
        rb2d.velocity = vel;
    }

    void UpdateAttackTimer()
    {
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
    }

    void ResetAttackTimer()
    {
        attackTimer = attackCoolDown;
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monk" && attackTimer <= 0f)
        {
            Debug.Log(gameObject + "attacks");
            ResetAttackTimer();
        }
    }
}
