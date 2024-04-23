using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    TextMeshPro blinkText;

    public float waitTime = 37.0f;


    private void Start()
    {
        blinkText = GetComponent<TextMeshPro>();
        StartBlinking();
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(waitTime);

        while (true)
        {
            switch (blinkText.color.a.ToString())
            {
                case "0":
                    blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine(Blink());
        StartCoroutine(Blink());
    }

    void StopBlinking()
    {
        StopCoroutine(Blink());
    }
}
