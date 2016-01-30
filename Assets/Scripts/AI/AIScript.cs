using System;
using UnityEngine;

abstract public class AIScript : MonoBehaviour {

    [SerializeField]
    protected float movementSpeed = 1f;
    [SerializeField]
    private float attackCoolDown = 1f;
    protected float attackTimer;

    private Transform monks;
    protected Transform targetMonk;
    protected Rigidbody2D rb2d;

    // Use this for initialization
    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        monks = GameObject.Find("Monks").transform;
        GetClosestMonk();
        ResetAttackTimer();
    }

    protected void GetClosestMonk()
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
        if (GameManagerScript.Enabled)
        {
            UpdateAttackTimer();
            Behave();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    protected virtual void Behave()
    {
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

    protected void ResetAttackTimer()
    {
        attackTimer = attackCoolDown;
    }
}
