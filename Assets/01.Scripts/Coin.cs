using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    int coin;
    [SerializeField] GameObject coinPrefab;
    int spawnTime = 5;

    private void Start()
    {
        StartCoroutine(CoinSpawn());
    }

    IEnumerator CoinSpawn()
    {
        while (true)
        {
            coin++;
            if(coin <= 6)
            {
                Vector3 randPos = new Vector3(Random.Range(-6, 26), Random.Range(5, 12), 0);
                PoolingManager.Instance.Respawn(coinPrefab, PoolingManager.Instance.coinPooler, 
                    randPos);
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}
