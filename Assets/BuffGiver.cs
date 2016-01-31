using UnityEngine;
using System.Collections;

public class BuffGiver : MonoBehaviour {

	[SerializeField]
	int buffindex = 0;

	[SerializeField]
	float cooldown = 30f;

	[SerializeField]
	float timeUntilActive = 0f;

	[SerializeField]
	bool unlocked = false;

	//static public int playerKillcount;

	// Use this for initialization
	void Start () 
	{
		//playerKillcount = 0;
	}
	
	
	void FixedUpdate () {
	
	}

	public void Unlock()
	{
		unlocked = true;
	}

	public void Lock()
	{
		unlocked = false;
	}

	void Update()
	{
		timeUntilActive -= Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (unlocked && timeUntilActive < 0f)
		{
			timeUntilActive = cooldown;
			other.GetComponent<Player> ().ApplyBuff (buffindex);
		}
	}
}
