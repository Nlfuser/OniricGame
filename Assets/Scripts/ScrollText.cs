using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour
{
    public float speed = 2f; // Adjust the speed as per your requirement

    private TextMeshProUGUI[] textMeshPros;

    void Start()
    {
        // Get all TextMeshPro components in children
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        foreach (TextMeshProUGUI textMeshPro in textMeshPros)
        {
            // Move each TMP text upwards
            textMeshPro.rectTransform.Translate(Vector3.up * speed * Time.deltaTime);

            // Reset the position when the text is outside the canvas
            if (textMeshPro.rectTransform.position.y > GetComponent<RectTransform>().rect.height)
            {
                // Adjust the position to start from the bottom
                textMeshPro.rectTransform.anchoredPosition = new Vector2(textMeshPro.rectTransform.anchoredPosition.x, -textMeshPro.preferredHeight);
            }
        }
    }
}
