using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int increaseAmount;


    private Player player;
    private int numSouls;
    private Button button;
    private bool isPurchased = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        button = gameObject.GetComponent<Button>();
        costText.text = "" + cost;
    }

    private void Update()
    {
        numSouls = GameManager.Instance.GetSouls();
        costText.text = cost + $" ({numSouls})";
        if(numSouls >= cost)
        {
            costText.color = Color.green;
        }
        else if(numSouls < cost)
        {
            costText.color = Color.red;
        }
    }

    //Set purchase button actions here

    //LVL 1
    public void OnSpeedLvlOneButton()
    {
        if (!isPurchased)
        {
            if (numSouls >= cost)
            {
                GameManager.Instance.DecreaseSouls(cost);
                player.IncreasePlayerSpeed(increaseAmount);
                SetPurchased();
            }
            else
            {
                Debug.Log("Not Enough Souls To Purchase");
            }
        }
    }

    public void OnHealthLvlOneButton()
    {
        if (!isPurchased)
        {
            if (numSouls >= cost)
            {
                GameManager.Instance.DecreaseSouls(cost);
                if (player.GetHP() == player.GetMaxHP())
                {
                    player.SetHP(player.GetMaxHP() + increaseAmount);
                }
                player.IncreasePlayerMaxHP(increaseAmount);
                SetPurchased();
            }
            else
            {
                Debug.Log("Not Enough Souls To Purchase");
            }
        }
    }

    public void OnDamageLvlOneButton()
    {
        if (!isPurchased)
        {
            if (numSouls >= cost)
            {
                GameManager.Instance.DecreaseSouls(cost);
                player.IncreasePlayerDamage(increaseAmount);
                SetPurchased();
            }
            else
            {
                Debug.Log("Not Enough Souls To Purchase");
            }
        }
    }

    //LVL 2
    public void OnHealthPointButton()
    {
        if (!isPurchased)
        {
            if (numSouls >= cost && player.GetHP() < player.GetMaxHP())
            {
                GameManager.Instance.DecreaseSouls(cost);
                player.IncreasePlayerHP(increaseAmount);
            }
            else
            {
                Debug.Log("Not Enough Souls To Purchase");
            }
        }
    }

    public void OnHealthLvlTwoButton()
    {
        if (!isPurchased)
        {
            if (numSouls >= cost)
            {
                GameManager.Instance.DecreaseSouls(cost);
                if (player.GetHP() == player.GetMaxHP())
                {
                    player.SetHP(player.GetMaxHP() + increaseAmount);
                }
                player.IncreasePlayerMaxHP(increaseAmount);
                SetPurchased();
            }
            else
            {
                Debug.Log("Not Enough Souls To Purchase");
            }
        }
    }

    public void OnDamageLvlTwoButton()
    {
        if (!isPurchased)
        {
            if (numSouls >= cost)
            {
                GameManager.Instance.DecreaseSouls(cost);
                player.IncreasePlayerDamage(increaseAmount);
                SetPurchased();
            }
            else
            {
                Debug.Log("Not Enough Souls To Purchase");
            }
        }
    }

    private void SetPurchased()
    {
        isPurchased = true;
        Color buttonColor = button.colors.normalColor;
        Color selectedColor = button.colors.selectedColor;
        buttonColor.a = 0.5f;
        selectedColor.a = 0.5f;
        ColorBlock cb = button.colors;
        cb.normalColor = buttonColor;
        cb.highlightedColor = buttonColor;
        cb.pressedColor = buttonColor;
        cb.selectedColor = selectedColor;
        button.colors = cb;
    }

}


