using UnityEngine;

public class RangeAIScript : AIScript
{
    [SerializeField]
    private float attackRange = 15f;
    [SerializeField]
    private float projectileSpeed = 5f;
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float attackDelay = 0.5f;
    private float attackDelayTimer;

    new void Start()
    {
        base.Start();
        ResetDelayTimer();
    }

    protected override void Behave()
    {
		if (targetMonk == null)
		{
			GetClosestMonk ();

			//no monks left
			if (targetMonk == null)
				return;
		}

        if (Vector2.Distance(targetMonk.position, transform.position) > attackRange)
        {
            base.Behave();
        }
        else
        {
            Attack();
        }
    }

    void Attack()
    {
        UpdateDelayTimer();
        GetClosestMonk();

        if (attackDelayTimer > 0f)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x + Random.Range(-0.1f, 0.1f), rb2d.velocity.y + Random.Range(-0.1f, 0.1f)).normalized;
        }

        if (attackTimer <= 0f && attackDelayTimer <= 0f)
        {
            rb2d.velocity = Vector2.zero;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            Vector2 projVel = (targetMonk.position - transform.position).normalized * projectileSpeed;
            projectile.GetComponent<Rigidbody2D>().velocity = projVel;
            ResetAttackTimer();
            ResetDelayTimer();
        }
    }

    void UpdateDelayTimer()
    {
        if (attackDelayTimer > 0f)
            attackDelayTimer -= Time.deltaTime;
    }

    void ResetDelayTimer()
    {
        attackDelayTimer = attackDelay;
    }
}
