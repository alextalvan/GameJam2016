using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonkManager : MonoBehaviour {


	[SerializeField]
	private List<Monk> _monks = new List<Monk>();



	// Use this for initialization
	void Start () 
	{
	
	}
	
	
	void FixedUpdate () 
	{
		
	}





	bool TestWinCondition()
	{
		for(int i=0;i<_monks.Count;++i)
		{
			if (!_monks [i].HasMaxCharge ())
			{
				return false;
			}
		}

		return true;
	}
}
