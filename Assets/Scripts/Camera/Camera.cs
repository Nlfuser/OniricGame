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
    private CinemachineVirtualCamera _cam;
    private float _previousPlayerDirection;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        DOTween.Init();
    }

    private void Update()
    {
        
        if (player.GetDir() > 0 && _previousPlayerDirection != player.GetDir())
        {
            StopCoroutine($"ChangeCameraDirection");
            StartCoroutine(ChangeCameraDirection(6f));
            _previousPlayerDirection = player.GetDir();
        }
        else if (player.GetDir() < 0 && _previousPlayerDirection != player.GetDir())
        {
            StopCoroutine($"ChangeCameraDirection");
            StartCoroutine(ChangeCameraDirection(-6f));
            _previousPlayerDirection = player.GetDir();
        }
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
    
    private IEnumerator ChangeCameraDirection(float targetX)
    {
        var framingTransposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        var startTime = Time.time;
        var startX = framingTransposer.m_TrackedObjectOffset.x;
    
        while (Time.time - startTime < duration)
        {
            framingTransposer.m_TrackedObjectOffset = new Vector3(Mathf.Lerp(startX, targetX, (Time.time - startTime) / duration), 3.25f, 0);
            yield return null;
        }
    
        framingTransposer.m_TrackedObjectOffset = new Vector3(targetX, 3.25f, 0);
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
