using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueTrigger : MonoBehaviour
{
// once player enters the trigger, it displays message 
// and destroys the trigger
private void OnTriggerEnter2D(Collider2D other) 
{
    if(other.tag == "Player")
    {
       GetComponent<DialogueTrigger>().TriggerDialogue();
    }    
}

}
