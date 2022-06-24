using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    /*void Start()
    {
        
    }

    void Update()
    {
        
    }*/

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Bullet"))
        {
            anim.SetTrigger("isHit");
        }
    }
}
