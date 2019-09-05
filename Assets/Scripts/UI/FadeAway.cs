using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAway : MonoBehaviour
{

    void Start()
    {
        print("Fuck!");
        Fade();
    }

    public void Fade()
    {
        StartCoroutine(FadeTo(0.0f, 1.0f));
      // StartCoroutine(FadeTo(1.0f, 1.0f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        CanvasRenderer[] childrenCanvasRenderers = GetComponentsInChildren<CanvasRenderer>();

        foreach (CanvasRenderer renderer in childrenCanvasRenderers)
        {
            float alpha = renderer.GetAlpha();
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime )
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
                renderer.SetAlpha(Mathf.Lerp(alpha, aValue, t));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
