using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;
    Vector3 dir;

    public GameObject explosionFactory;

    void Start()
    {

        int randValue = UnityEngine.Random.Range(0, 10); // 0 ~ 9
        if (randValue < 3)
        {
            GameObject target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        else
        {
            dir = Vector3.down;
        }
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject smObject = GameObject.Find("ScoreManager");
        ScoreManager sm = smObject.GetComponent<ScoreManager>();
        sm.SetScore(sm.GetScore() + 1);

        GameObject explsionFactory = Instantiate(explosionFactory);
        explsionFactory.transform.position = transform.position;

        Destroy(gameObject);
        Destroy(collision.gameObject);

        if(collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
