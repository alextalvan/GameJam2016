using UnityEngine;

public class MeleeAIScript : AIScript
{
    [SerializeField]
	private int attackDamage = 10;

	[SerializeField]
	private int chargeDamage = 10;

	MonkManager monkmng;

	[SerializeField]
	Animator _animator;

	protected override void Start()
	{
		base.Start ();
		monkmng = GameObject.Find ("Monks").GetComponent<MonkManager> ();
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monk" && attackTimer <= 0f)
        {
            //Debug.Log(gameObject + "attacks");
			Monk m = coll.gameObject.GetComponent<Monk>();
			m.Stun ();
			monkmng.RemoveCharge (chargeDamage);
			m.Damage (attackDamage);
            audioS.Play();

            //m.RemoveCharge (attackDamage);
            ResetAttackTimer();

			_animator.Play ("meleeAttack");
        }
    }
}
