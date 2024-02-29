using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] private float transitionDuration = 2.0f; // Set your desired transition duration here
    [SerializeField] private Material inkTransitionMaterial;
    private bool _isTransitioning = false;
    private static readonly int Amount = Shader.PropertyToID("_Amount");
    private static LocalKeyword _leftSide;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _leftSide = new LocalKeyword(inkTransitionMaterial.shader, "_LEFTSIDE");
        sceneLoaded += TransitionOut;
    }

    public void StartTransition(string sceneName, Action onComplete = null)
    {
        if(_isTransitioning)
            return;
        _isTransitioning = true;
        inkTransitionMaterial.SetKeyword(_leftSide, false);
        inkTransitionMaterial.SetFloat(Amount, 0);
        
        StartCoroutine(Transition(0f, 1f, () => 
        {
            onComplete?.Invoke();
            LoadScene(sceneName);
        }));
    }
    
    private void TransitionOut(Scene scene, LoadSceneMode mode)
    {
        if (instance == null || this == null)
            return;
        if(!_isTransitioning)
            return;
        inkTransitionMaterial.SetKeyword(_leftSide, true);
        inkTransitionMaterial.SetFloat(Amount, 1);

        StartCoroutine(Transition(1f, 0f, () => _isTransitioning = false));
    }

    private IEnumerator Transition(float from, float to, Action onComplete = null)
    {
        var startTime = Time.time;
        while (Time.time - startTime < transitionDuration)
        {
            var t = (Time.time - startTime) / transitionDuration;
            var amount = Mathf.Lerp(from, to, t);
            inkTransitionMaterial.SetFloat(Amount, amount);
            yield return null;
        }

        inkTransitionMaterial.SetFloat(Amount, to);
        
        onComplete?.Invoke();
    }


    public bool IsTransitioning()
    {
        return _isTransitioning;
    }
}
