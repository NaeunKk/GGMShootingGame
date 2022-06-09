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

    /// <summary>
    /// 생성 함수
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
            yield return new WaitForSeconds(1);
        }
    }
}
