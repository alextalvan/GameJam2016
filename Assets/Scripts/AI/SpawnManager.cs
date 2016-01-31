using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private float waveSpawnCoolDown = 1f;
    private int waveStrength = 1;
    private float spawnTimer;
    [SerializeField]
    private GameObject meleeAiPrefab;
    [SerializeField]
    private GameObject rangeAiPrefab;
    [SerializeField]
    private float waveModifier = 1f;
    private Transform AIs;
    private MonkManager mm;
    private bool reset = true;

    // Use this for initialization
    void Start()
    {
        ResetTimer();
        AIs = GameObject.Find("AIs").transform;
        mm = GameObject.Find("Monks").GetComponent<MonkManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManagerScript.Enabled)
        {
            waveStrength = (int)(mm.GetRelativeCharge() * waveModifier);
            UpdateTimer();
            SpawnAI();
        }
    }

    void SpawnAI()
    {
        if (reset && AIs.childCount == 0)
        {
            ResetTimer();
            reset = false;
        }

        if (spawnTimer <= 0f && AIs.childCount == 0)
        {
            for (int i = 0; i < waveStrength; i++)
            {
                GameObject rndAI = GetRandomAIType();
                Vector3 rndPos = GetRandomSpawnPoint();
                GameObject newAI = Instantiate(rndAI, rndPos, Quaternion.identity) as GameObject;
                newAI.transform.parent = AIs;
                reset = true;
            }
        }
    }

    GameObject GetRandomAIType()
    {
        int rndIndex = Random.Range(0, 2);
        GameObject rndAI = null;
        switch (rndIndex)
        {
            case 0:
                rndAI = meleeAiPrefab;
                break;
            case 1:
                rndAI = rangeAiPrefab;
                break;
        }
        return rndAI;
    }

    Vector3 GetRandomSpawnPoint()
    {
        int rndIndex = Random.Range(0, 4);
        Transform rndSpawn = transform.GetChild(rndIndex);

        Vector3 rndSpawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndSpawnPoint = rndSpawn.TransformPoint(rndSpawnPoint * 0.5f);
        return rndSpawnPoint;
    }

    void UpdateTimer()
    {
        if (spawnTimer > 0f)
            spawnTimer -= Time.deltaTime;
    }

    void ResetTimer()
    {
        spawnTimer = waveSpawnCoolDown;
    }
}
