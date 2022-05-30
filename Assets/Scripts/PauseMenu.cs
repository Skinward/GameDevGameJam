using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void OnResume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OnQuit()
    {
        Application.Quit();
    }


}
