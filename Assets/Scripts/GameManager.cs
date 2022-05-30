using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI soulsText;
    [SerializeField] private GameObject pauseMenu;

    private Player player;
    private int souls = 0;

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

    private void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void AddSoul()
    {
        souls++;
    }

    private void UpdateUI()
    {
        healthText.text = player.GetHP() + " / " + player.GetMaxHP();
        soulsText.text = "" + souls;
    }

    public int GetSouls()
    {
        return souls;
    }

    public void DecreaseSouls(int amount)
    {
        souls -= amount;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
