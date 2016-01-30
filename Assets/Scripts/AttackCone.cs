using UnityEngine;
using System.Collections;

public class AttackCone : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	
	void FixedUpdate () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("AI"))
			GameObject.Destroy (other.gameObject);
	}
}
