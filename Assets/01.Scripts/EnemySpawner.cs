using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPos = new List<Transform>();
    [SerializeField] GameObject enemyPrefab;

    int randIndex = 0;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            if (Enemy.enemyCount < 10)
            {
                randIndex = Random.Range(0, spawnPos.Count);

                //Vector3 randPos = new Vector3(Random.Range(-7, 28),
                //Random.Range(12, 6), 0);
                PoolingManager.Instance.Respawn(enemyPrefab,
                    PoolingManager.Instance.enemyPooler, spawnPos[randIndex].position);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
