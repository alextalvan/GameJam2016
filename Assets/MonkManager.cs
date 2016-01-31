using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonkManager : MonoBehaviour {

	[SerializeField]
	int _currentCharge;

	[SerializeField]
	int _maxCharge = 3000;

	[SerializeField]
	private List<BuffGiver> _buffs = new List<BuffGiver>();


	public void RemoveCharge(int amount)
	{
		_currentCharge -= amount;

		if (_currentCharge < 0)
			_currentCharge = 0;
	}

    public int GetCharge
    {
        get { return _currentCharge; }
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


	void HandleBuffs()
	{
		if (GetRelativeCharge () >= 0.25f)
			_buffs [0].Unlock ();
		else
			_buffs [0].Lock ();

		if (GetRelativeCharge () >= 0.50f)
			_buffs [1].Unlock ();
		else
			_buffs [1].Lock ();

		if (GetRelativeCharge () >= 0.75f)
			_buffs [2].Unlock ();
		else
			_buffs [2].Lock ();
	}

}
