using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    [SerializeField] private float creditShowTime = 3f;

    void Start()
    {
        StartCoroutine(EndCredits());
    }

    private IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(creditShowTime);
        SceneManager.LoadScene(0);
    }
    
}
