using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region �Ѿ� ������
    [Header("�Ѿ� ������")]
    [SerializeField] private float speed;
    [SerializeField] Rigidbody2D rb;
    #endregion

    //void OnEnable()
    //{
    //    rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    //}

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        Vector2 minPos = GameManager.instance.minPos.position;
        Vector2 maxPos = GameManager.instance.maxPos.position;
        Vector2 pos = transform.position;

        if (pos.x > maxPos.x || pos.x < minPos.x || pos.y > maxPos.y || pos.y < minPos.y)
            PoolManager1.Instance.Enqueue(gameObject);
    }
}
