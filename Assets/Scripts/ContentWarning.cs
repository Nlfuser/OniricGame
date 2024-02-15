using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutCanvas : MonoBehaviour
{
    [SerializeField]
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        // Get the CanvasGroup component if not assigned
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        // Start the fading process
        StartCoroutine(FadeOutCanvasRoutine());
    }

    IEnumerator FadeOutCanvasRoutine()
    {
        // Set the initial alpha value to fully visible
        canvasGroup.alpha = 1f;

        // Fade out the Canvas over time
        float timer = 0f;
        while (timer < fadeDuration)
        {
            // Calculate the current alpha value based on the fade duration
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            canvasGroup.alpha = alpha;

            // Update the timer
            timer += Time.deltaTime;
            yield return null;
        }

        // Disable the Canvas once faded out
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
