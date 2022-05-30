using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControllerReemergence : MonoBehaviour

{
[SerializeField] Sprite[] treeSprites;
private int treeHits = 0;
public void OnTreeHit()
{
    treeHits++;
    gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[treeHits-1];

    if(treeHits == 3)
    {   
        gameObject.GetComponent<SpriteRenderer>().flipX=true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
}