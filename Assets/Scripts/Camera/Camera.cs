using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float duration;
    [SerializeField] private float sprintDuration = 1f;
    [SerializeField] private Ease ease;
    private CinemachineVirtualCamera _cam;
    private float _previousPlayerDirection;
    private float _elapsedTime;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        DOTween.Init();
    }

    private void Update()
    {
        var framingTransposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _elapsedTime = 0f;
        while (_elapsedTime < duration)
        {
            float newAmount = 0;
            if (player.stateOfPlayer == Player.PlayerGameState.run)
            {
                newAmount = Mathf.Lerp(framingTransposer.m_TrackedObjectOffset.x, 6f * player.GetDir(), _elapsedTime);
            }
            framingTransposer.m_TrackedObjectOffset = new Vector3(newAmount, 3.25f, 0);
            _elapsedTime += Time.deltaTime;
        }
        /*
        else if (_previousPlayerDirection != player.GetDir() && player.stateOfPlayer == Player.PlayerGameState.walk)
        {
            DOVirtual.Float(framingTransposer.m_TrackedObjectOffset.x, 6f * player.GetDir(), duration, value => {
                framingTransposer.m_TrackedObjectOffset = new Vector3(value, 3.25f, 0);
            }).SetEase(ease);
            _previousPlayerDirection = player.GetDir();
        }*/
    }

    private void LateUpdate()
    {
        if (player.StartedRunning())
        {
            StopCoroutine($"ChangeOrthographicSize");
            StartCoroutine(ChangeOrthographicSize(6f));
        }
        if (player.EndedRunning())
        {
            StopCoroutine($"ChangeOrthographicSize");
            StartCoroutine(ChangeOrthographicSize(5f));
        }
    }

    private IEnumerator ChangeOrthographicSize(float targetSize)
    {
        var startTime = Time.time;
        var startSize = _cam.m_Lens.OrthographicSize;
    
        while (Time.time - startTime < sprintDuration)
        {
            _cam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, (Time.time - startTime) / sprintDuration);
            yield return null;
        }
    
        _cam.m_Lens.OrthographicSize = targetSize;
                   
    }
}
