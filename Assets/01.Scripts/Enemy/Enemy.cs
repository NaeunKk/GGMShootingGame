using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region 생성 관련
    [Header("생성 관련")]
    public static int enemyCount;
    Rigidbody2D rb;
    private float thinkTime;
    #endregion
    #region 움직임 관련
    [Header("움직임 관련")]
    [SerializeField] private int nextMove;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Transform targetTrm;
    bool isFollow = false;
    float speed = 5f;
    Vector3 dir;
    #endregion
    #region 피격 관련
    [Header("피격 관련")]
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] protected Animator anim;
    #endregion

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetTrm = GameObject.Find("Player").GetComponent<Transform>();
        Think();
    }

    protected void OnEnable()
    {
        currentHp = maxHp;
        enemyCount++;
    }

    protected void OnDisable()
    {
        enemyCount--;
    }

    protected void FixedUpdate()
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

    protected void Update()
    {
        if(nextMove >= 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(nextMove <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(isFollow)
        {
            if (transform.position.x < targetTrm.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// 적 방향 전환 함수
    /// </summary>
    protected void Think()
    {
        nextMove = UnityEngine.Random.Range(-1, 2);

        float nextThinkTime = UnityEngine.Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isFollow = true;
            StartCoroutine(ColorChange());
            currentHp--;
            StartCoroutine(StopFollow());
            PoolManager1.Instance.Enqueue(collision.gameObject);
        }

        if (currentHp == 0)
        {
            PoolManager1.Instance.Enqueue(gameObject);
            isFollow = false;
            sr.color = Color.white;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        print("끼어요!");
    //    }
    //}

    /// <summary>
    /// 피격 시 색상 전환 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorChange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    protected IEnumerator StopFollow()
    {
        yield return new WaitForSeconds(3);
        isFollow = false;
    }
}
