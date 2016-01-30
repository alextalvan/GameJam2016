using UnityEngine;
using System.Collections;

public class Monk : Damageable 
{

	//[SerializeField]
	//int _currentCharge;

	//[SerializeField]
	//int _maxCharge = 1000;

	MonkManager _manager;

	[SerializeField]
	int _chargeSpeed = 1;

	bool _allowCharge = true;

	[SerializeField]
	float interruptDuration = 1f;

	float interruptTimer = 0f;


	// Use this for initialization
	void Start () 
	{
		GetComponent<Animator> ().speed = 0.1f;
		_manager = GameObject.Find ("Monks").GetComponent<MonkManager> ();
	}
	
	
	void FixedUpdate () 
	{
		IncrementCharge ();

		//debug
		//if (HasMaxCharge ())
		//	Debug.Log ("monk finished");
	}

	void Update()
	{
		interruptTimer -= Time.deltaTime;

		if (interruptTimer <= 0f)
			_allowCharge = true;
	}

	/*
	public bool HasMaxCharge()
	{
		return _currentCharge == _maxCharge;
	}*/

	public void Stun()
	{
		_allowCharge = false;
		interruptTimer = interruptDuration;
	}

	/*
	public void RemoveCharge(int amount)
	{
		_currentCharge -= amount;

		if (_currentCharge < 0)
			_currentCharge = 0;
	}
	*/


	void IncrementCharge()
	{
		if (_allowCharge)
		{
			_manager.IncrementCharge (_chargeSpeed);
		}

	}
		

	void OnCollisionEnter2D()
	{
		Stun ();
	}

}
