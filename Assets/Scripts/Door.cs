using System;
using UnityEngine;

public class Door : Selectable
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool walkThrough;

    private void Update()
    {
        if (!walkThrough && IsMouseOver() && IsPlayerClicking() && !SceneManager.instance.IsTransitioning())
            SceneManager.instance.StartTransition(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(walkThrough && col.CompareTag("Player") && !SceneManager.instance.IsTransitioning())
            SceneManager.instance.StartTransition(sceneToLoad);
    }
}
