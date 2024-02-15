using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MovingText : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float fadeSpeed = 1.0f;

    private TextMeshProUGUI textMeshPro;
    private float alpha = 1.0f;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // Start the fading process
        StartCoroutine(FadeAndMove());
    }

    IEnumerator FadeAndMove()
    {
        // Move up continuously
        while (true)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            // Fade out over time
            alpha -= fadeSpeed * Time.deltaTime;
            textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, alpha);

            // If text is fully transparent, destroy the object
            if (alpha <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("HospitalRoom");
                break;
            }

            yield return null;
        }
    }
}
