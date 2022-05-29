using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnTime = 2;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            if(Enemy.enemyCount < 10)
            {
            Vector3 randPos = new Vector3(Random.Range(-7, 28), Random.Range(12, 6), 0);
            PoolingManager.Instance.Respawn(enemy, PoolingManager.Instance.enemyPooler, randPos);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
