using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject EnemyPrefab1;
    [SerializeField]
    private float Enemy1Interval = 5.0f;
    [SerializeField]
    private int enemiesPerWave = 2;
    [SerializeField]
    private int maxEnemies = 10;
    [SerializeField]
    private float spawnRadius = 10f; 

    private int enemiesSpawned = 0;

    void Start()
        //this script is a failure that didnt end up getting used properly
    {
        StartCoroutine(spawnEnemy(Enemy1Interval, EnemyPrefab1));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);

        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (enemiesSpawned >= maxEnemies)
            {
                Debug.Log("Max enemies reached!");
                yield break;
            }

            Vector3 spawnPosition = GetRandomNavMeshPosition();

            if (spawnPosition != Vector3.zero)
            {
                GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                enemiesSpawned++;
                Debug.Log($"Spawned enemy {enemiesSpawned}/{maxEnemies}");
            }
            else
            {
                Debug.LogWarning("Failed to find valid NavMesh position!");
            }
        }

        if (enemiesSpawned < maxEnemies)
        {
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else
        {
            Debug.Log("All enemies spawned!");
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        
        for (int i = 0; i < 30; i++) // Try 30 times
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-5f, 5f),
                0, 
                Random.Range(-6f, 6f)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas))
            {
                return hit.position; 
            }
        }

        Debug.LogWarning("Could not find NavMesh position after 30 attempts");
        return Vector3.zero;
    }
}