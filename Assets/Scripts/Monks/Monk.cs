using UnityEngine;
using System.Collections;

public class Monk : MonoBehaviour 
{

	[SerializeField]
	float _currentCharge;

	[SerializeField]
	float _maxCharge = 1000f;


	// Use this for initialization
	void Start () 
	{
		GetComponent<Animator> ().speed = 0.1f;
	}
	
	
	void FixedUpdate () 
	{
		
	}
}
