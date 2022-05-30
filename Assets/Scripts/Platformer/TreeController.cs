using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TreeController : MonoBehaviour
{
    new Light2D light;
[SerializeField] Sprite[] treeSprites;
private int treeHits = 0;
private void Start() 
{
    light = GetComponentInChildren<Light2D>();
}
public void OnTreeHit()
{
    treeHits++;
    gameObject.GetComponent<SpriteRenderer>().sprite = treeSprites[treeHits-1];

    if(treeHits == 3)
    {
        FindObjectOfType<Player>().GetComponent<Player>().PlatformerDie();
        StartCoroutine(EndScene());
        //flip for reemergence
        //gameObject.GetComponent<SpriteRenderer>().flipX=true;
    }
}

    private IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2f);
        float time = 0f;
        while(time < 3000)
        {
            light.intensity += 0.02f;
            yield return new WaitForSeconds(1 / 3000);
            time += 1;
        }
    }
}
