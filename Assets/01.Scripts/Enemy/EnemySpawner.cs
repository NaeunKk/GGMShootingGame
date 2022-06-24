using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPos = new List<Transform>();
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject boss;
    int randIndex = 0;
    int spawnDelay = 1;
    int bossSpawnDelay = 30;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator Spawn()
    {
        while (true)
        {
            if (Enemy.enemyCount < 10)
            {
                randIndex = Random.Range(0, spawnPos.Count);

                //Vector3 randPos = new Vector3(Random.Range(-7, 28),
                //Random.Range(12, 6), 0);
                GameObject temp = PoolManager1.Instance.Dequeue(enemyPrefab);
                temp.transform.position = spawnPos[randIndex].position;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    IEnumerator BossSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossSpawnDelay);
            GameObject temp = PoolManager1.Instance.Dequeue(boss);
            temp.transform.position = new Vector3(25, 13, 0);
        }
    }
}
