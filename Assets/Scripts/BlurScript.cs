using UnityEngine;

public class BlurScript : MonoBehaviour {

    [SerializeField]
    private GameObject UI;
    private UnityStandardAssets.ImageEffects.MotionBlur motionBlur;
    private bool fade = false;
    private bool add = false;

	// Use this for initialization
	void Start () {
        motionBlur = GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        FadeBlur();
        AddBlur();
    }

    void FadeBlur()
    {
        if (fade)
        {
            add = false;
            if (motionBlur.blurAmount > 0f)
            {
                motionBlur.blurAmount -= Time.deltaTime;
                if (UI.activeSelf)
                    UI.SetActive(false);
            }
            else
            {
                fade = false;
                if (!GameManagerScript.Enabled)
                    GameManagerScript.Enabled = true;
                //Destroy(motionBlur);
                //Destroy(this);
            }
        }
    }

    void AddBlur()
    {
        if (add)
        {
            fade = false;
            if (motionBlur.blurAmount < 0.92f)
            {
                motionBlur.blurAmount += Time.deltaTime;
            }
            else
            {
                add = false;
                if (!UI.activeSelf)
                    UI.SetActive(true);
                //Destroy(motionBlur);
                //Destroy(this);
            }
        }
    }

    public bool SetFade
    {
        set { fade = value; }
    }

    public bool SetAdd
    {
        set { add = value; }
    }
}
