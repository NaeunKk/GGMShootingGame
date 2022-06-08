using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollback : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private Vector3 respawnPos = new Vector3(-17, 3, 0);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.transform.position = respawnPos;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
