using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour 
{
	public delegate void DamageableDeath ();

	public event DamageableDeath OnDeath;

	[SerializeField]
	int currentHealth = 100;

	[SerializeField]
	int maxHealth = 100;

	// Use this for initialization
	void Start () {
	
	}

	public void Damage(int amount)
	{
		currentHealth -= amount;

		if (currentHealth < 0)
		{
			if (OnDeath != null)
				OnDeath ();

			currentHealth = 0;
		}
	}

	public void Heal(int amount)
	{
		currentHealth += amount;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}

	public float GetRelativeHealth()
	{
		return (float)currentHealth / maxHealth;
	}
	
	void FixedUpdate () 
	{
	
	}
}
