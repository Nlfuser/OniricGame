using System;
using DG.Tweening;
using UnityEngine;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] private GameObject pauseScreen;

    private bool _isPaused;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;
            GetComponent<CanvasGroup>().DOFade(_isPaused ? 1f : 0f, 0.5f).SetUpdate(true);
        }
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    public void Pause()
    {
        _isPaused = true;
        GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetUpdate(true);
    }

    public void Resume()
    {
        _isPaused = false;
        GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetUpdate(true);
    }
    
    public bool IsPaused()
    {
        return _isPaused;
    }

    public void Quit()
    {
        SceneManager.instance.StartTransition("", Application.Quit);
    }
}
