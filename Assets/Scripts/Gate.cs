using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private string message = "A key is needed\nto open the gate.";

    public void ShowMessage()
    {
        text.text = message;
    }

    public void OpenGate()
    {
        Destroy(gameObject);
    }


}
