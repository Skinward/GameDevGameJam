using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject upgradesCanvas;
    [SerializeField] private TextMeshProUGUI soulText;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        soulText.text = "" + GameManager.Instance.GetSouls();
    }

    public void ShowPurchaseUI()
    {
        upgradesCanvas.SetActive(!upgradesCanvas.activeInHierarchy);

        if (upgradesCanvas.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
    }

}
