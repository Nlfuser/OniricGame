using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Selectable
{
    public string sceneToLoad;

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking())
        {
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(sceneToLoad);
    }
}