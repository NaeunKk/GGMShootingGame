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
        Vector3 randPos = new Vector3(Random.Range(-6, 26), Random.Range(28, 6), 0);
        if (collision.collider.CompareTag("Player"))
        {
            player.transform.position = respawnPos;
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("aa");
            collision.transform.position = randPos;
        }
        else if (collision.collider.CompareTag("Coin"))
        {
            collision.transform.position = randPos;
        }
    }
}
