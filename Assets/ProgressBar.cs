using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	[SerializeField]
	MonkManager _monk;

	Vector3 initialScale;

	Renderer _renderer;
	//SpriteRenderer _renderer;
	// Use this for initialization
	void Start () 
	{
		//_renderer = GetComponent<SpriteRenderer> ();
		_renderer = GetComponent<Renderer>();
		initialScale = transform.localScale;
	}


	void Update () 
	{
		float relative = _monk.GetRelativeCharge();

		/*
		Vector3 newscale = new Vector3 (initialScale.x * relative,
			initialScale.y,
			initialScale.z);

		transform.localScale = newscale;

		Vector3 c = Vector3.Lerp (new Vector3 (1, 0, 0), new Vector3 (0, 1, 0),relative);
		_renderer.color = new Color (c.x, c.y, c.z, 1);
		*/

		_renderer.material.SetFloat ("_Width", relative);
	}
}
