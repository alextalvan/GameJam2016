using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	[SerializeField]
	private int chargeDamage = 10;

	[SerializeField]
	private int attackDamage = 10;

	MonkManager monkmng;

	void Start()
	{
		
		monkmng = GameObject.Find ("Monks").GetComponent<MonkManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monk")
        {
            //Debug.Log("HIT");

			Monk m = coll.collider.GetComponent<Monk> ();
			m.Stun();
			m.Damage (attackDamage);

			monkmng.RemoveCharge (chargeDamage);

        }
        Destroy(gameObject);
    }
}
