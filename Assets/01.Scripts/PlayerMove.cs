using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnrealEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] private Animator move;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int hp;
    [SerializeField] private Text hpTxt; 
    [SerializeField] AudioSource foot;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject gameOver;

    BoxCollider2D collider;
    Rigidbody2D rb;
    bool isJumping;
    Vector2 overlapPos;
    bool isGround;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        foot = GetComponent<AudioSource>();
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

        MoveSound();
        Jump();
    }

   void MoveSound()
    {
        foot.Play();
    }
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
    IEnumerator ifHit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
}
