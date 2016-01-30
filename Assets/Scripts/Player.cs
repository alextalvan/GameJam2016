using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	[SerializeField]
	float _speed = 5f;

	Rigidbody2D _rigid;

	//attacking
	[SerializeField]
	GameObject _attackHelper;

	[SerializeField]
	float attackCooldownDuration = 0.05f;

	float attackCooldownTimer = 0f;
	float attackDurationTimer = 0f;

	[SerializeField]
	float attackDuration = 0.05f;

	[SerializeField]
	int dashesBeforeCooldown = 3;

	int _currentDashCount;

	//dashing
	Vector2 _storedDashDir = Vector2.zero;
	[SerializeField]
	float _dashForce = 2000f;

	[SerializeField]
	float _dashFalloff = 0.9f;

	float _currentDashStrength = 0f;

	bool _dashing = false;


	//blink
	float blinkCooldownTimer = 0f;
	[SerializeField]
	float _blinkCooldown = 0f;

	[SerializeField]
	float blinkDelay = 0.25f;

	float blinkDelayCooldownTimer = 0f;

	bool _startedBlink = false;

	Vector2 storedBlinkDestination = Vector2.zero;


	// Use this for initialization
	void Start () 
	{
		_rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
	}

	void Update()
	{
		UpdateTimers ();

		HandleMovement ();
		HandleAttack ();
		HandleBlink ();

		//if (_rigid.velocity.magnitude > 50f)
		//	_rigid.velocity = _rigid.velocity.normalized * 50f;
	}

	void UpdateTimers()
	{
		attackCooldownTimer -= Time.deltaTime;
		attackDurationTimer -= Time.deltaTime;
		blinkCooldownTimer -= Time.deltaTime;
		blinkDelayCooldownTimer -= Time.deltaTime;
	}

	void HandleBlink()
	{
		if (Input.GetMouseButtonDown (1) && blinkCooldownTimer <= 0f)
		{
			_startedBlink = true;
			
			storedBlinkDestination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//transform.position = mPos;

			blinkDelayCooldownTimer = blinkDelay;
			blinkCooldownTimer = _blinkCooldown;

		}

		if (_startedBlink && blinkDelayCooldownTimer <= 0f)
		{
			_startedBlink = false;
			transform.position = storedBlinkDestination;
		}
	}




	void HandleAttack()
	{
		if (attackDurationTimer <= 0f)
		{
			_attackHelper.SetActive (false);
			_dashing = false;
		}

		if (attackCooldownTimer <= 0f)
		{
			_currentDashCount = 0;
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);


			Vector2 diff = mPos - _rigid.position;
			//Debug.Log (diff.normalized);

			float angle = (float)Mathf.Atan2 (diff.y, diff.x);

			_attackHelper.transform.localPosition = diff.normalized * 0.64f;


			_attackHelper.transform.rotation = Quaternion.Euler (0, 0, angle * 180f / Mathf.PI);


			if (_currentDashCount < dashesBeforeCooldown)
			{
				attackDurationTimer = attackDuration;
				attackCooldownTimer = attackCooldownDuration;
				_attackHelper.SetActive (true);

				_storedDashDir = diff.normalized;
				_currentDashStrength = _dashForce;
				_dashing = true;
				_currentDashCount++;
			}

		}

		_rigid.velocity += _storedDashDir * _currentDashStrength;
		_currentDashStrength *= _dashFalloff;

	}
		


	void HandleMovement()
	{
		if (_dashing)
			return;

		_rigid.velocity = Vector2.zero;

		Vector2 dir = new Vector2 (0, 0);

		if (Input.GetKey (KeyCode.A))
		{
			dir.x--;
		}

		if (Input.GetKey (KeyCode.D))
		{
			dir.x++;
		}

		if (Input.GetKey (KeyCode.S))
		{
			dir.y--;
		}

		if (Input.GetKey (KeyCode.W))
		{
			dir.y++;
		}

		//_rigid.AddForce (dir.normalized * _speed * Time.deltaTime);
		_rigid.velocity = dir.normalized * _speed;

		if (_rigid.velocity.magnitude > 50f)
			_rigid.velocity = _rigid.velocity.normalized * 50f;


	}
}
