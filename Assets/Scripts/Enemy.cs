using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float triggerRange = 5f;
    [SerializeField] private float enemyHp = 3f;
    [SerializeField] private GameObject soulPrefab;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private bool isKeyHolder = false;
    [SerializeField] private AudioClip deathSound;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private bool chasingPlayer = false;
    private new AudioSource audio;


    void Start()
    {
        playerTransform = FindObjectOfType<Player>().gameObject.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //If Player is within certain range then go towards them
        if (Mathf.Abs(Vector3.Distance(transform.position, playerTransform.position)) < triggerRange)
        {
            chasingPlayer = true;
        }

        if (chasingPlayer)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);

        //If the player is more than 2 unit away from the enemy follow otherwise stay back a bit
        if (Mathf.Abs(Vector3.Distance(transform.position, playerTransform.position)) > 0.25f)
        {
            Vector2 positionToMoveTo =  playerPosition - (Vector2)transform.position;
            positionToMoveTo.Normalize();
            rb.MovePosition((Vector2)transform.position + (positionToMoveTo * moveSpeed * Time.deltaTime));
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        //Flip the enemy sprite to track the player
        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction.Normalize();
        if(direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            var damageAmount = other.gameObject.GetComponentInParent<Player>().GetDamageAmount();
            TakeDamage(damageAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Rune Puzzle"))
        {
            Debug.Log(other.gameObject.tag);
            other.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rune Puzzle"))
        {
            other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void TakeDamage(float amount)
    {
        enemyHp -= amount;

        if(enemyHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(soulPrefab, transform.position, Quaternion.identity);
        if (isKeyHolder)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
        }
        audio.PlayOneShot(deathSound);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

}
