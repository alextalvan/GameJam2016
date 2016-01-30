using UnityEngine;

public class BlurScript : MonoBehaviour {

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
            }
            else
            {
                fade = false;
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
