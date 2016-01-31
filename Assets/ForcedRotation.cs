using UnityEngine;
using System.Collections;

public class ForcedRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update () 
	{
		transform.eulerAngles = new Vector3 (0, 0, 0);
	}
}
