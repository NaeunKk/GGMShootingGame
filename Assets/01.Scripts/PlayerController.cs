using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnrealEngine;

public class PlayerMove : MonoBehaviour
{
    #region 움직임 관련
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Animator move;
    //[SerializeField] AudioSource foot;
    bool isJumping;
    bool isGround;
    Rigidbody2D rb;
    Vector2 overlapPos;
    #endregion

    #region 피격 관련
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Text hpTxt; 
    [SerializeField] private int hp;
    BoxCollider2D collider;
    #endregion

    #region 점수 관련
    [SerializeField] GameObject coin;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameClear;
    #endregion

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //foot = GetComponent<AudioSource>();
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
        float h = Input.GetAxis("Horizontal") ;

        Vector2 dir = new Vector2 (h, 0);
        transform.Translate(dir * speed * Time.deltaTime);

        if (Input.GetAxis("Horizontal") != 0)
        {
            move.SetBool("IsMoving", true);
        }
        else
        {
            move.SetBool("IsMoving", false);
        }

        //MoveSound();
        Jump();

        if (hp >= 50)
            GameClear.SetActive(true);
    }

   /*void MoveSound()
    {
        foot.Play();
    }*/

   /// <summary>
   /// 점프 함수
   /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            move.SetTrigger("isJumping");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp--;
            StartCoroutine(ifHit());
        }

        if(hp == 0)
        {
            JsonManager.instance.Save();
            gameOver.SetActive(true);
            GameManager.instance.DisplayScore();
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.PlusScore();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Rollback"))
            hp--;
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
}
