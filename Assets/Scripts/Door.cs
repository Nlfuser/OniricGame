using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Door : Selectable
{
    public string sceneToLoad;
    public float transitionDuration = 2.0f; // Set your desired transition duration here
    public Material inkTransitionMaterial;
    private float currentTransitionTime = 0.0f;

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking())
        {
            StartCoroutine(TransitionAndLoadScene());
        }
    }

    IEnumerator TransitionAndLoadScene()
    {

        inkTransitionMaterial.SetKeyword(new LocalKeyword(inkTransitionMaterial.shader, "_LEFTSIDE"), false);

        float startTime = Time.time;
        while (currentTransitionTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            currentTransitionTime = Mathf.Lerp(0.0f, transitionDuration, t);

            // Update the amount slider on the ink transition material
            float amount = Mathf.Lerp(0.0f, 1.0f, currentTransitionTime / transitionDuration);
            inkTransitionMaterial.SetFloat("_Amount", amount);

            yield return null;
        }

        // Ensure the amount is set to 1 at the end of the loop
        inkTransitionMaterial.SetFloat("_Amount", 1.0f);

        startTime = Time.time;
        currentTransitionTime = 0.0f;

        inkTransitionMaterial.SetKeyword(new LocalKeyword(inkTransitionMaterial.shader, "_LEFTSIDE"), true);

        while (currentTransitionTime < transitionDuration)
        {
            float t = (Time.time - startTime) / transitionDuration;
            currentTransitionTime = Mathf.Lerp(0.0f, transitionDuration, t);

            // Update the amount slider on the ink transition material
            float amount = Mathf.Lerp(1.0f, 0.0f, currentTransitionTime / transitionDuration);
            inkTransitionMaterial.SetFloat("_Amount", amount);

            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);

        // Ensure the amount is set to 0 at the end of the loop
        inkTransitionMaterial.SetFloat("_Amount", 0.0f);
    }
}
