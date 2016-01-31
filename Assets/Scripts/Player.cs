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

	public int CurrentDashDount { get { return _currentDashCount; } }

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

	public float GetRelativeAttackCooldownTimer { get { return attackCooldownTimer / attackCooldownDuration; } }



	//buffs
	float[] bufftimers = new float[3];
	float _initDashForce;
	float _initBlinkDelay;
	CircleCollider2D attackCollider;
	float initAttackRadius;


	//sprites
	[SerializeField]
	SpriteRenderer _spriteContainer;

	[SerializeField]
	Sprite idle;

	[SerializeField]
	Sprite forward;

	[SerializeField]
	Sprite down;

	[SerializeField]
	Sprite up;

	[SerializeField]
	float transparency_strength = 75f;

	// Use this for initialization
	void Start () 
	{
		_rigid = GetComponent<Rigidbody2D> ();

		for (int i = 0; i < 3; ++i)
			bufftimers [i] = 0f;

		_initDashForce = _dashForce;
		_initBlinkDelay = blinkDelay;

		attackCollider = _attackHelper.GetComponent<CircleCollider2D> ();
		initAttackRadius = attackCollider.radius;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
	}

	void Update()
    {
        if (GameManagerScript.Enabled)
        {
            UpdateTimers();

            HandleMovement();
            HandleAttack();

            HandleBlink();
			HandleBuffs ();

			UpdateSprite ();


        }
		//if (_rigid.velocity.magnitude > 50f)
		//	_rigid.velocity = _rigid.velocity.normalized * 50f;
	}

	void UpdateTimers()
	{
		attackCooldownTimer -= Time.deltaTime;
		attackDurationTimer -= Time.deltaTime;
		blinkCooldownTimer -= Time.deltaTime;
		blinkDelayCooldownTimer -= Time.deltaTime;

		for (int i = 0; i < 3; ++i)
			bufftimers [i] -= Time.deltaTime;
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

		if (Input.GetKeyDown(KeyCode.RightShift)||Input.GetKeyDown(KeyCode.H)||Input.GetMouseButtonDown(0))
		{
			Vector2 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);


			Vector2 diff = mPos - _rigid.position;
			//Debug.Log (diff.normalized);

			float angle = (float)Mathf.Atan2 (diff.y, diff.x);

			//_attackHelper.transform.localPosition = diff.normalized * 0.64f;


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

	public void ApplyBuff(int index)
	{
		if (index == 0)
		{
			_dashForce = _initDashForce * 1.25f;
		}

		if (index == 1)
		{
			blinkDelay = 0f;
		}

		if (index == 2)
		{
			attackCollider.radius = initAttackRadius * 1.5f;
		}

		bufftimers [index] = 10.0f;
	}

	private void StopBuff(int index)
	{
		if (index == 0)
		{
			_dashForce = _initDashForce;
		}

		if (index == 1)
		{
			blinkDelay = _initBlinkDelay;
		}

		if (index == 2)
		{
			attackCollider.radius = initAttackRadius;
		}
	}


	void HandleBuffs()
	{
		for (int i = 0; i < 3; ++i)
		{
			if (bufftimers [i] < 0f)
			{
				StopBuff (i);
			}
		}
	}


	void UpdateSprite()
	{
		Vector2 v = _rigid.velocity;


		_spriteContainer.flipX = (v.x < 0);


		if (v.magnitude <= 2f)
		{
			_spriteContainer.sprite = idle;
			return;
		}

		/*
		if (v.x > 6 * v.y)
		{
			_spriteContainer.sprite = forward;
			return;
		}
		else
		{
			if(v.y > 0)
			{
				_spriteContainer.sprite = down;
				return;
			}
			else
			{
				_spriteContainer.sprite = up;
				return;
			}
		}
		*/

		_spriteContainer.color = new Color (1f, 1f, 1f, 1f - v.magnitude/transparency_strength);

		if(_currentDashCount == 1)
		{
			_spriteContainer.sprite = up;
			return;
		}

		if(_currentDashCount == 2)
		{
			_spriteContainer.sprite = down;
			return;
		}

		if(_currentDashCount == 3)
		{
			_spriteContainer.sprite = forward;
			return;
		}


	}
}
