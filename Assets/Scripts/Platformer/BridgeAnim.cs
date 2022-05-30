using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAnim : MonoBehaviour
{

   private Animator anim;
void OnEnable() 
{
    anim = GetComponent<Animator>();
}
public void BridgeAnimation()
{
    anim.SetTrigger("isDown");
}
}
