using UnityEngine;

public class MeleeAIScript : AIScript
{
    [SerializeField]
    private float attackDamage = 1f;

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monk" && attackTimer <= 0f)
        {
            Debug.Log(gameObject + "attacks");
            ResetAttackTimer();
        }
    }
}
