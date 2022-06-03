using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount;
    Rigidbody2D rb;
    private float thinkTime;
    [SerializeField] private int nextMove;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] Transform targetTrm;

    bool isFollow = false;
    float speed = 5f;
    Vector3 dir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetTrm = GameObject.Find("Player").GetComponent<Transform>();
        Think();
    }

    private void OnEnable()
    {
        currentHp = maxHp;
        enemyCount++;
    }
    private void OnDisable()
    {
        enemyCount--;
    }
    void FixedUpdate()
    {
        if (!isFollow)
        {
            rb.velocity = new Vector2(nextMove, rb.velocity.y);
        }
        else
        {
            dir = new Vector3(targetTrm.position.x - transform.position.x, 0, 0);
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
        Vector2 frontVec = new Vector2(rb.position.x + nextMove, rb.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, layerMask);
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", thinkTime);
        }
    }
    private void Update()
    {
        if(nextMove >= 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(nextMove <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(isFollow)
        {
            if (transform.position.x < targetTrm.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);

        }
    }

    void Think()
    {
        nextMove = UnityEngine.Random.Range(-1, 2);

        float nextThinkTime = UnityEngine.Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isFollow = true;
            StartCoroutine(ColorChange());
            currentHp--;
            StartCoroutine(StopFollow());
            Destroy(collision.gameObject);
        }

        if (currentHp == 0)
        {
            Destroy(gameObject);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        print("³¢¾î¿ä!");
    //    }
    //}
    IEnumerator ColorChange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
    IEnumerator StopFollow()
    {
        yield return new WaitForSeconds(3);
        isFollow = false;
    }
}
