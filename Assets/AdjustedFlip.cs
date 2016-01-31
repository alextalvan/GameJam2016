using UnityEngine;
using System.Collections;

public class AdjustedFlip : MonoBehaviour {

	[SerializeField]
	Rigidbody2D _rigidbody;

	[SerializeField]
	SpriteRenderer _renderer;

	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update () 
	{
		_renderer.flipX = (_rigidbody.velocity.x < 0);
	}
}
