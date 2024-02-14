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

    private bool isTransitioning = false; // Flag to check if the transition is in progress

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking() && !isTransitioning)
        {
            StartCoroutine(TransitionAndLoadScene());
        }
    }

    IEnumerator TransitionAndLoadScene()
    {
        isTransitioning = true;

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

        StartCoroutine(TransitionOut());
    }

    IEnumerator TransitionOut()
    {
        float startTime = Time.time;
        currentTransitionTime = 0.0f;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Continue the transition while the scene is being loaded asynchronously
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

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

        // Ensure the amount is set to 0 at the end of the loop
        inkTransitionMaterial.SetFloat("_Amount", 0.0f);

        isTransitioning = false; // Reset the flag to allow new transitions
    }
}
