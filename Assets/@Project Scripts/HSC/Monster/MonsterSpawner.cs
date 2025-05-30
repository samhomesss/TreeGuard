using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 3f;

    private float timer = 0f;
    private bool isDestroyed = false;

    public string type;

    void Update()
    {
        if (isDestroyed) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            SpawnEnemyNear();
        }
    }

    void SpawnEnemyNear()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        Vector3 spawnPosition = transform.position + randomOffset;

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void DestroySpawner()
    {
        isDestroyed = true;

        if(type == "1")
        {
            WaveManager.Instance.stopType1Wave = true; // Stop type 1 wave
        }
        else if (type == "2")
        {
            WaveManager.Instance.stopType2Wave = true; // Stop type 2 wave
        }

    }
}
