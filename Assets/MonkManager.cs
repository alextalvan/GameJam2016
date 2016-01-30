using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonkManager : MonoBehaviour {

	[SerializeField]
	int _currentCharge;

	[SerializeField]
	int _maxCharge = 3000;

	//[SerializeField]
	//private List<Monk> _monks = new List<Monk>();


	public void RemoveCharge(int amount)
	{
		_currentCharge -= amount;

		if (_currentCharge < 0)
			_currentCharge = 0;
	}



	public void IncrementCharge(int amount)
	{
			_currentCharge += amount;

			if (_currentCharge > _maxCharge)
				_currentCharge = _maxCharge;

	}

	public float GetRelativeCharge()
	{
		return (float)_currentCharge / _maxCharge;
	}


	// Use this for initialization
	void Start () 
	{
	
	}
	
	
	void FixedUpdate () 
	{
		
	}





	bool TestWinCondition()
	{
		return _currentCharge == _maxCharge;
	}
}
