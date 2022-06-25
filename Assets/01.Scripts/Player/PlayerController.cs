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
    [SerializeField] float setTime = 180;
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
    public float currentHp;
    [SerializeField] float maxHp;
    BoxCollider2D collider;
    #endregion

    #region 점수 관련
    [SerializeField] float hungryTime = 3f;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameClear;
    [SerializeField] Image hungryGage;
    [SerializeField] AudioSource eatingSound;
    #endregion

    bool isDie = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        foot = GetComponent<AudioSource>();
        currentHp = maxHp;
    }

    private void Start()
    {
        StartCoroutine(PlayTime());
        StartCoroutine(Hungry());
    }

    void Update()
    {
        hpTxt.text = $"HP : {currentHp}";

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

        Jump();

        if (currentHp <= 0 && !isDie)
        {
            isDie = true;
            GameOverTextStart();
        }
    }

    int index = 1;

    void GameOverTextStart()
    {
        gameOver.SetActive(true);
        Sequence seq = DOTween.Sequence();

        if (currentHp <= 0 && index == 1)
        {
            seq.Append(gameOverTxt.DOText("공룡의 허기를 채우지 못했어요...", 5f));
            index--;
        }
        if (currentHp >= 0 && index == 1)
        {
            seq.Append(gameOverTxt.DOText("공룡의 허기를 채워줬어요!!", 5f));
            index--;
        }
        seq.AppendCallback(() => Time.timeScale = 0);
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
            currentHp--;
            hungryGage.fillAmount = currentHp / maxHp;
            StartCoroutine(ifHit());
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            eatingSound.Play();
            currentHp++;
            hungryGage.fillAmount = currentHp / maxHp;
            PoolManager1.Instance.Enqueue(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Rollback"))
            currentHp--; hungryGage.fillAmount = currentHp / maxHp;
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
            if (setTime <= 60)
            {
                sec = (int)setTime;
                time.text = $"{sec} s";
            }
            if (setTime <= 0)
            {
                GameOverTextStart();
            }
        }
    }

    IEnumerator Hungry()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungryTime);
            currentHp--;
            hungryGage.fillAmount = currentHp / maxHp;
        }
    }
}
