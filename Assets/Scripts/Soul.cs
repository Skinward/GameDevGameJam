using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canTravel = false;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<Player>().gameObject.transform;
    }

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(MoveToPlayer());
    }

    private void Update()
    {
        if (canTravel)
        {
            TravelToPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddSoul();
            StopCoroutine(MoveToPlayer());
            Destroy(gameObject);
        }
    }

    private void TravelToPlayer()
    {
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction.Normalize();
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(0.75f);
        canTravel = true;
    }

}
