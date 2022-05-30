using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = FindObjectOfType<Player>().gameObject.GetComponent<PlayerInput>();
        notificationText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            
            //Set the text for button press based on keeb or controller
            if (playerInput.currentControlScheme == "Keyboard&Mouse")
            {
                notificationText.text = "Press E";
            }
            if (playerInput.currentControlScheme == "Gamepad")
            {
                notificationText.text = "Press B";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            notificationText.text = "";
        }

    }

    public void ClearText()
    {
        notificationText.text = "";
    }

}
