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
		attackCooldownTimer -= Time.deltaTime;
		attackDurationTimer -= Time.deltaTime;

		HandleMovement ();
		HandleAttack ();


	}



	void HandleAttack()
	{
		if (attackDurationTimer <= 0f)
		{
			_attackHelper.SetActive (false);
		}

		if (Input.GetMouseButtonDown (0))
		{
			Vector2 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);


			Vector2 diff = mPos - _rigid.position;
			//Debug.Log (diff.normalized);

			float angle = (float)Mathf.Atan2 (diff.y, diff.x);

			_attackHelper.transform.localPosition = diff.normalized * 0.64f;


			_attackHelper.transform.rotation = Quaternion.Euler (0, 0, angle * 180f / Mathf.PI);


			if (attackCooldownTimer <= 0f)
			{
				
				attackCooldownTimer = attackCooldownDuration;
				attackDurationTimer = attackDuration;
				_attackHelper.SetActive (true);
			}
				
		}


	}


	



























	void HandleMovement()
	{
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
		_rigid.velocity = dir.normalized * _speed * Time.deltaTime;
	}
}
