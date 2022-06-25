using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
//using UnrealEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Text gameOverTxt;
    #region Play Time
    [SerializeField] Text time;
    float setTime = 180;
    int min;
    int sec;
    #endregion

    #region 움직임 관련
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Animator move;
    [SerializeField] AudioSource foot;
    bool isJumping;
    bool isGround;
    Rigidbody2D rb;
    Vector2 overlapPos;
    #endregion

    #region 피격 관련
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Text hpTxt;
    public int hp;
    BoxCollider2D collider;
    #endregion

    #region 점수 관련
    [SerializeField] float hungryTime = 3f;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameClear;
    [SerializeField] Image hungryGage;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        foot = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PlayTime());
        StartCoroutine(Hungry());
    }

    void Update()
    {
        hpTxt.text = $"HP : {hp}";

        Bounds bounds = collider.bounds;
        overlapPos = new Vector2(bounds.center.x, bounds.min.y);
        isGround = Physics2D.OverlapCircle(overlapPos, 0.1f, layerMask);

        if (isGround)
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
        float h = Input.GetAxis("Horizontal");

        Vector2 dir = new Vector2(h, 0);
        transform.Translate(dir * speed * Time.deltaTime);

        if (Input.GetAxis("Horizontal") != 0)
        {
            move.SetBool("IsMoving", true);
        }
        else
        {
            move.SetBool("IsMoving", false);
        }

        MoveSound();
        Jump();

        if(hp <= 0)
            gameOver.SetActive(true);
    }

    void MoveSound()
    {
        if (isGround == true)
            foot.Play();
    }

    /// <summary>
    /// 점프 함수
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            move.SetTrigger("isJumping");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            Debug.Log("A");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp--;
            hungryGage.fillAmount -= 0.05f;
            StartCoroutine(ifHit());
        }
/*
        if (hp == 0)
        {
            JsonManager.instance.Save();
            gameOver.SetActive(true);
            //GameManager.instance.DisplayScore();
        }*/

        if (collision.gameObject.CompareTag("Coin"))
        {
            hp++;
            hungryGage.fillAmount += 0.05f / hp;
            PoolManager1.Instance.Enqueue(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Rollback"))
            hp--; hungryGage.fillAmount -= 0.05f / hp;
    }

    /// <summary>
    /// 데미지 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator ifHit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    IEnumerator PlayTime()
    {
        while (true)
        {
            yield return null;
            setTime -= Time.deltaTime;

            if (setTime >= 60)
            {
                min = (int)setTime / 60;
                sec = (int)setTime % 60;
                time.text = $"{min} m {sec} s";
            }
            else if (setTime <= 60)
            {
                sec = (int)setTime;
                time.text = $"{sec} s";
            }
            else if (setTime <= 0)
            {
                gameOver.SetActive(true);
                if (hp <= 0)
                    gameOverTxt.DOText("공룡의 허기를 채우지 못했어요...", 5f);
                else if (hp >= 0)
                    gameOverTxt.DOText("공룡의 허기가 채워졌어요!!", 5f);
            }
        }
    }

    IEnumerator Hungry()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungryTime);
            hp -= 1;
            hungryGage.fillAmount -= 0.05f / hp;
        }
    }
}
