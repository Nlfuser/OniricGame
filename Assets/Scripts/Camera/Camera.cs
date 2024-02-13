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

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        DOTween.Init();
    }

    private void Update()
    {
        var framingTransposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (_previousPlayerDirection != player.GetDir() && (player.stateOfPlayer == Player.PlayerGameState.walk || player.stateOfPlayer == Player.PlayerGameState.run))
        {
            int walking = player.stateOfPlayer == Player.PlayerGameState.walk? 0: 1;
            DOVirtual.Float(framingTransposer.m_TrackedObjectOffset.x, 6f * player.GetDir(), duration, value => {
                framingTransposer.m_TrackedObjectOffset = new Vector3(value * walking, 3.25f, 0);
            }).SetEase(ease);
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
