using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneWall : MonoBehaviour
{
    [SerializeField] private RuneTrigger[] runeTriggers;

    void Update()
    {
        if(runeTriggers[0].GetIsActivated() && runeTriggers[1].GetIsActivated() && runeTriggers[2].GetIsActivated())
        {
            Destroy(gameObject);
        }
    }
}
