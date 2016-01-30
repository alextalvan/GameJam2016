using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private float spawnCoolDown = 1f;
    private float spawnTimer;
    [SerializeField]
    private GameObject aiPrefab;
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

    Vector3 GetRandomSpawnPoint()
    {
        int rndIndex = Random.Range(0, 4);
        Transform rndSpawn = transform.GetChild(rndIndex);

        Vector3 rndSpawnPoint = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndSpawnPoint = rndSpawn.TransformPoint(rndSpawnPoint * 0.5f);
        return rndSpawnPoint;
    }

    void SpawnAI()
    {
        if (spawnTimer <= 0f)
        {
            Vector3 rndPos = GetRandomSpawnPoint();
            GameObject newAI = Instantiate(aiPrefab, rndPos, Quaternion.identity) as GameObject;
            newAI.transform.parent = AIs;
            ResetTimer();
        }
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
