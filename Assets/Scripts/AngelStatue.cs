using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class AngelStatue : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] private TextMeshProUGUI notificationText;
    new Light2D light;
    
    private string message = "Enemies nearby\ncannot free the angels";

    private void Start()
    {
        light = GetComponentInChildren<Light2D>();
    }

    private void ShowMessage()
    {
        notificationText.text = message;
    }

    public void ReleaseAngels()
    {
        int enemiesRemaining = 0;
        foreach (var enemy in enemies)
        {
            if(enemy != null)
            {
                enemiesRemaining++;
            }
        }

        if (enemiesRemaining > 0)
        {
            ShowMessage();
        }
        else
        {
            StartCoroutine(EndScene());
        }
    }

    private IEnumerator EndScene()
    {
        notificationText.text = "";
        yield return new WaitForSeconds(0.5f);
        float time = 0f;
        while(time < 700)
        {
            light.intensity += 0.07f;
            yield return new WaitForSeconds(1 / 700);
            time += 1;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
