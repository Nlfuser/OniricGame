using UnityEngine;

public class Door : Selectable
{
    [SerializeField] private string sceneToLoad;

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking() && !SceneManager.instance.IsTransitioning())
            SceneManager.instance.StartTransition(sceneToLoad);
    }
}
