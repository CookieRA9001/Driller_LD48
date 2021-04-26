using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessScript : MonoBehaviour
{
    private bool seen;
    public float fadeMulti = 1f;
    public SpriteRenderer sRenderer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!seen && other.CompareTag("Player")) {
            seen = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime*fadeMulti)
        {
            sRenderer.color = new Color(sRenderer.color.r, sRenderer.color.g, sRenderer.color.b, sRenderer.color.a * i);
            yield return null;
        }
    }
}
