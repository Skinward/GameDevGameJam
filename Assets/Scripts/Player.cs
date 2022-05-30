using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private int playerMaxHP = 3;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private AudioClip sword;
    [SerializeField] private AudioClip treeHit;
    
    private int playerHP;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;
    private bool isTopDown;
    private string interactableName = "";
    private GameObject interactableObject;
    private bool isGrounded;
    private bool hasKey = false;
    private bool isAlive = true;
    private MenuManager menuManager;
    private new AudioSource audio;
    

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerHP = playerMaxHP;
        menuManager = FindObjectOfType<MenuManager>();
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            isTopDown = true;
        }
        else
        {
            isTopDown = false;
        }
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        else
        {
            MovePlayer();
        }

        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void MovePlayer()
    {
        //Platform Controls
        if (!isTopDown)
        {
            //Horizontal movement with sprite flipping correct way
             Vector2 runVelocity = new Vector2 (moveInput.x *moveSpeed , rb.velocity.y);
             rb.velocity = runVelocity;
             FlipSprite();

             //bool to see if moving then setting it to that bool
              bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
              anim.SetBool("isWalking", playerHasHorizontalSpeed);

            if (!isGrounded)
            {
                anim.SetBool("isWalking",false);
            }
             

        }
        

        //Top Down Controls
        if (isTopDown)
        {
            if (moveInput.x != 0 || moveInput.y != 0)
            {
                
                //movement
                Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                if (Time.timeScale != 0)
                {
                    if (moveInput.x > 0)
                    {
                        transform.rotation = new Quaternion(0, 0, 0, 1);
                    }
                    else if (moveInput.x < 0)
                    {
                        transform.rotation = new Quaternion(0, 180, 0, 1);
                    }
                }
                rb.velocity = playerVelocity;
                anim.SetBool("isWalking", true);
            }
            else
            {
                //stop movement but maintain last rotation
                rb.velocity = Vector2.zero;
                anim.SetBool("isWalking", false);
            }
        }
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
             transform.localScale=  new Vector2 (Mathf.Sign(rb.velocity.x), 1f); 
        }
    }

    public void OnInteract()
    {   if (!isAlive)
        {
            return;
        }
        else
        {
          if (interactableName.Equals(""))
        {
            return;
        }

        //Set action for purchase shrine
        if (interactableName.Equals("Purchase Shrine"))
        {
            menuManager.SetFirstSelectedPurchase();
            UIManager.Instance.ShowPurchaseUI();
        }

        //Set action for gate
        if (interactableName.Equals("Gate"))
        {
            Gate gate = interactableObject.GetComponent<Gate>();
            if (hasKey)
            {
                gate.OpenGate();
                hasKey = false;
            }
            else
            {
                gate.ShowMessage();
            }
        }

        //Set action for angels
        if (interactableName.Equals("Angels"))
        {
            AngelStatue angels = interactableObject.GetComponent<AngelStatue>();
            angels.ReleaseAngels();
        }

        //Set action for key
        if (interactableName.Equals("Key"))
        {
            Destroy(interactableObject);
            hasKey = true;
        }

        //Set action for bridge
        if (interactableName.Equals("Bridge"))
        {  
            FindObjectOfType<BridgeAnim>().gameObject.GetComponent<BridgeAnim>().BridgeAnimation();
                interactableObject.GetComponent<Interactable>().ClearText();
        }

        if(interactableName.Equals("Tree"))
        {
            TreeController tc = interactableObject.GetComponent<TreeController>();
            tc.OnTreeHit();
            Debug.Log("Chop chop");
            anim.SetTrigger("isWoodcutting");
        }  

        if(interactableName.Equals("ReverseTree"))
        {
            TreeControllerReemergence tc = interactableObject.GetComponent<TreeControllerReemergence>();
            tc.OnTreeHit();
            Debug.Log("Chop chop");
            anim.SetTrigger("isWoodcutting");
        }  
        }
        
    }

    public void OnJump(InputValue value)
        {
            //Platform Controls
             if (!isAlive)
        {
            return;
        }
        else
        {
            if (!isTopDown)
            { 
                if (!isGrounded)
                { 
                    return;
                }
                if(value.isPressed & isGrounded)
                {            
                    anim.SetBool("isJumping", true);
                    rb.velocity += new Vector2 (0f, jumpHeight);
                    isGrounded = false;
                }
           
            }
        }
          
        }
    private void OnCollisionEnter2D(Collision2D other)
    {
          //Platformer part
         if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded= true;
             anim.SetBool("isJumping", false);
        }
        if (other.gameObject.CompareTag("Water"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.CompareTag("EndingWall"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Check Enemy Collision
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //check for sprite renderer, if null return
        if (other.gameObject.GetComponent<SpriteRenderer>() == null)
        {
            return;
        }

        //check if the object is an interactable
        if (other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals("Interactable"))
        {
            //get the object tag and set interactableName 
            interactableName = other.gameObject.tag;
            interactableObject = other.gameObject;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        interactableName = "";
    }

    public void OnFire()
    {
        if(isTopDown)
        {
            anim.SetTrigger("Attack");
            audio.PlayOneShot(sword);
        }
        
        if(!isTopDown)
        {
             if (!isAlive)
        {
            return;
        }
        else
        {
            anim.SetTrigger("isAttacking");
        }       
        }     
    }

    public void OnPause()
    {
        menuManager.SetFirstSelectedPause();
        GameManager.Instance.Pause();
    }

    private void TakeDamage()
    {
        playerHP--;
        if(playerHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetHP()
    {
        return playerHP;
    }

    public void SetHP(int amount)
    {
        playerHP = amount;
    }

    public int GetMaxHP()
    {
        return playerMaxHP;
    }

    public int GetDamageAmount()
    {
        return damageAmount;
    }

    public void IncreasePlayerHP(int amount)
    {
        playerHP += amount;
    }

    public void IncreasePlayerMaxHP(int amount)
    {
        playerMaxHP += amount;
    }

    public void IncreasePlayerDamage(int amount)
    {
        damageAmount += amount;
    }

    public void IncreasePlayerSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public bool GetHasKey()
    {
        return hasKey;
    }

    public void SetHasKey(bool value)
    {
        hasKey = value;
    }

    public void PlatformerDie()
    {
        anim.SetTrigger("isDead");
        isAlive = false;

        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
