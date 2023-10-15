using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    WaitForSeconds Delay04 = new WaitForSeconds(0.4f);
    Renderer RR;

    void Start()
    {
        RR = GetComponent<Renderer>();
        StartCoroutine(HideRendererCo());
    }

    IEnumerator HideRendererCo()
    {
        while (true)
        {
            RR.enabled = (PlayerPos.playerPos - transform.position).sqrMagnitude < 2116;
            yield return Delay04;
        }
    }
}
