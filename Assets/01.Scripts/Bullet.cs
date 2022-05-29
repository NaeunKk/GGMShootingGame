using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Rigidbody2D rb;

    //void OnEnable()
    //{
    //    rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    //}
    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        Vector2 minPos = PoolingManager.Instance.minPos.position;
        Vector2 maxPos = PoolingManager.Instance.maxPos.position;
        Vector2 pos = transform.position;

        if (pos.x > maxPos.x || pos.x < minPos.x || pos.y > maxPos.y || pos.y < minPos.y)
            PoolingManager.Instance.Despwan(gameObject, PoolingManager.Instance.bulletPooler);
    }
}
