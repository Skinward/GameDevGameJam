using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    [SerializeField] float moveSpeed = 1f;
    void Start()
    {
        anim= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2 (moveSpeed,0);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Ground")
    {
        moveSpeed= -moveSpeed;
        FlipEnemyFacing();
    }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            anim.SetTrigger("isAttacking");
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale=  new Vector2 (-(Mathf.Sign(rb.velocity.x)), 1f); 
    }

    public void EnemyDeath()
    {
        anim.SetTrigger("isDead");
        Destroy(gameObject);
    }
}