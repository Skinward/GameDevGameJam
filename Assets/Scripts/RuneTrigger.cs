using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RuneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject triggerBlock;
    [SerializeField] private GameObject rune;

    private new Light2D light;
    private float lightStartingIntensity;
    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        light = rune.GetComponentInChildren<Light2D>();
        lightStartingIntensity = light.intensity;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject == triggerBlock)
        {
            light.intensity = 1.2f;
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        light.intensity = lightStartingIntensity;
        if (other.gameObject == triggerBlock)
        {
            isActivated = false;
        }
    }

    public bool GetIsActivated()
    {
        return isActivated;
    }
}
