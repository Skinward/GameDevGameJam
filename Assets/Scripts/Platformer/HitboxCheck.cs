using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCheck : MonoBehaviour
{
  Collider2D hitbox;
  private void OnEnable() 
  {
      hitbox = GetComponent<CapsuleCollider2D>();
  }
public void OnTriggerStay2D(Collider2D other) 
{
    if(other.tag == "Platformer_Enemies")
    {
        FindObjectOfType<Platformer_Enemy>().gameObject.GetComponent<Platformer_Enemy>().EnemyDeath();
    }
}
}
