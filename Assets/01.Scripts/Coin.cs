using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    #region
    [SerializeField] GameObject coinPrefab;
    [SerializeField] private float[] yLevel;
    public int coin;
    int spawnTime = 4;
    #endregion

    private void Start()
    {
        StartCoroutine(CoinSpawn());
    }

    /// <summary>
    /// 코인 생성 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CoinSpawn()
    {
        if (coin <= 6)
        {
            while (true)
            {
                float tgtY = 5.5f;//yLevel[Random.Range(0, yLevel.Length)];
                coin++;
                Vector3 randPos = new Vector3(Random.Range(-6, 26), tgtY, 0);
                GameObject temp = PoolManager1.Instance.Dequeue(coinPrefab);
                temp.transform.position = randPos;
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else if (coin > 6)
            yield return null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("끼어요!");
        }
    }
}
