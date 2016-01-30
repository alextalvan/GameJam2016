using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    static private bool gameEnabled = false;

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private BlurScript blur;

    static public bool Enabled
    {
        get { return gameEnabled; }
        set { gameEnabled = value; }
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        ChangeGameState();
    }

    void ChangeGameState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameEnabled)
            {
                gameEnabled = false;
                blur.SetAdd = true;
                UI.SetActive(true);
            }
            else
            {
                gameEnabled = true;
                blur.SetFade = true;
                UI.SetActive(false);
            }
        }
    }
}
