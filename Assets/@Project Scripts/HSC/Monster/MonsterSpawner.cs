using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 3f;

    private float timer = 0f;
    private bool isDestroyed = false;

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
    }
}
