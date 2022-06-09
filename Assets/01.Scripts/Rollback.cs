using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollback : MonoBehaviour
{
    #region GameObject
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject coin;
    #endregion

    #region 리스폰 포지션
    [Header("리스폰 포지션")]
    [SerializeField] private Vector3 respawnPos = new Vector3(-17, 3, 0);
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.transform.position = respawnPos;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            PoolManager1.Instance.Enqueue(enemy);
        }
        if (collision.collider.CompareTag("Coin"))
        {
            PoolManager1.Instance.Enqueue(coin);
        }
    }
}
