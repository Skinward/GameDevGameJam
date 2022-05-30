using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPickups : MonoBehaviour
{
  bool wasCollected = false;

  //if the player is touching, mark as collected, and "pickup"/destroy object
  //worth nothing so far in tutorial
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" & !wasCollected)
        {
            wasCollected=true;
            Destroy(gameObject);
        }
    }
}
