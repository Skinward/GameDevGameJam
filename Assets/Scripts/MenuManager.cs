using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject purchaseSelectedButton;
    [SerializeField] GameObject pauseSelectedButton;
    
    EventSystem eventSystem;



    public void SetFirstSelectedPurchase()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(purchaseSelectedButton, new BaseEventData(eventSystem));
    }

    public void SetFirstSelectedPause()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(pauseSelectedButton, new BaseEventData(eventSystem));
    }
}
