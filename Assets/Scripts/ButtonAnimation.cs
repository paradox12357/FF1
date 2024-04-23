using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public float scaleFactor = 1.2f;
    public float duration = 0.2f; // Duration for the animation
    private Vector3 originalScale;
    private Coroutine currentCoroutine;

    void Start()
    {
        // Save the original scale of the button
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If there is an ongoing animation, stop it
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Start the enlargement animation
        currentCoroutine = StartCoroutine(AnimateScale(originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If there is an ongoing animation, stop it
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Start the deflation animation
        currentCoroutine = StartCoroutine(AnimateScale(originalScale));
    }

    private IEnumerator AnimateScale(Vector3 targetScale)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            // Update the scale of the button
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);

            // Wait for the next frame
            yield return null;

            // Update the time
            time += Time.deltaTime;
        }

        // Ensure the target scale is set exactly
        transform.localScale = targetScale;
    }
}
