using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject EnemyPrefab1;

    [SerializeField]
    private float Enemy1Interval = 1.0f;

    void Start()
    {
        StartCoroutine(spawnEnemy(Enemy1Interval, EnemyPrefab1)); //copy with the other varaibles if add more enemy lateru
    }

    private IEnumerator spawnEnemy(float  interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
