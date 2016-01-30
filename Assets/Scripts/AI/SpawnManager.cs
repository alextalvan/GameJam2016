using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private float spawnCoolDown = 1f;
    private float spawnTimer;
    [SerializeField]
    private GameObject meleeAiPrefab;
    [SerializeField]
    private GameObject rangeAiPrefab;
    private Transform AIs;

	// Use this for initialization
	void Start () {
        ResetTimer();
        AIs = GameObject.Find("AIs").transform;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateTimer();
        SpawnAI();
    }

    void SpawnAI()
    {
        if (spawnTimer <= 0f)
        {
            GameObject rndAI = GetRandomAIType();
            Vector3 rndPos = GetRandomSpawnPoint();
            GameObject newAI = Instantiate(rndAI, rndPos, Quaternion.identity) as GameObject;
            newAI.transform.parent = AIs;
            ResetTimer();
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
        spawnTimer = spawnCoolDown;
    }
}
