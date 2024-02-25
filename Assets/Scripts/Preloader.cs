using UnityEditor;
using UnityEngine;

[InitializeOnLoad, ExecuteAlways]
public static class Preloader
{
    static Preloader() => EditorApplication.playModeStateChanged += Load;

    private static void Load(PlayModeStateChange state)
    {
        var index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }
    }
}
