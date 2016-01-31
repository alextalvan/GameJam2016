using UnityEngine;
using System.Collections;

public class PlayerCooldownBar : MonoBehaviour {

	[SerializeField]
	Player _player;

	Vector3 initialScale;

	SpriteRenderer _renderer;
	// Use this for initialization
	void Start () 
	{
		_renderer = GetComponent<SpriteRenderer> ();
		initialScale = transform.localScale;
	}


	void Update () 
	{
		float relative = _player.GetRelativeAttackCooldownTimer;

		int dashCount = _player.CurrentDashDount;

		if (relative < 0f)
			relative = 0f;

		Vector3 newscale = new Vector3 (initialScale.x * relative,
			initialScale.y,
			initialScale.z);

		transform.localScale = newscale;

		//if (dashCount == 0) _renderer.color = new Color(0,0,0,0);
		if (dashCount == 1) _renderer.color = Color.green;
		if (dashCount == 2) _renderer.color = Color.yellow;
		if (dashCount == 3) _renderer.color = Color.red;
	}
}
