using UnityEngine;

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
            }
            else
            {
                blur.SetFade = true;
            }
        }
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
}
