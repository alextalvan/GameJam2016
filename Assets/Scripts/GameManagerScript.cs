using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    static private bool gameEnabled = false;

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private BlurScript blur;

    private Transform monks;

    static public bool Enabled
    {
        get { return gameEnabled; }
        set { gameEnabled = value; }
    }

	// Use this for initialization
	void Start () {
        monks = GameObject.Find("Monks").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckGameState();
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

    private void CheckGameState()
    {
        if (monks.childCount == 0)
        {
            GameOver(false);
        }
        else if (monks.GetComponent<MonkManager>().TestWinCondition())
        {
            GameOver(true);
        }
    }

    public void GameOver(bool won)
    {
        if (won)
        {
            gameEnabled = false;
            blur.SetAdd = true;
            UI.transform.GetChild(2).gameObject.SetActive(false);
            UI.transform.GetChild(5).gameObject.SetActive(true);
        }
        else
        {
            gameEnabled = false;
            blur.SetAdd = true;
            UI.transform.GetChild(2).gameObject.SetActive(false);
            UI.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
}
