using UnityEngine;
using System.Collections;

public class MonkChargeBar : MonoBehaviour {

	[SerializeField]
	Monk _monk;

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
        if (GameManagerScript.Enabled)
        {
            float relative = _monk.GetRelativeHealth();

            Vector3 newscale = new Vector3(initialScale.x * relative,
                                               initialScale.y,
                                               initialScale.z);

            transform.localScale = newscale;

            Vector3 c = Vector3.Lerp(new Vector3(1, 0, 0), new Vector3(0, 1, 0), relative);
            _renderer.color = new Color(c.x, c.y, c.z, 1);
        }
	}
}
