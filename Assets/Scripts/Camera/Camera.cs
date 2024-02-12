using System;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float duration;
    private CinemachineVirtualCamera _cam;
    private float _elapsedTime;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        var framingTransposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (player.GetDir() > 0)
        {
            _elapsedTime = 0f;
            while (_elapsedTime < duration)
            {
                var newAmount = Mathf.Lerp(framingTransposer.m_TrackedObjectOffset.x, 6f, _elapsedTime);
                framingTransposer.m_TrackedObjectOffset = new Vector3(newAmount, 3.25f, 0);
                _elapsedTime += Time.deltaTime;
            }
        }
        else if (player.GetDir() < 0)
        {
            _elapsedTime = 0f;
            while (_elapsedTime < duration)
            {
                var newAmount = Mathf.Lerp(framingTransposer.m_TrackedObjectOffset.x, -6f, _elapsedTime);
                framingTransposer.m_TrackedObjectOffset = new Vector3(newAmount, 3.25f, 0);
                _elapsedTime += Time.deltaTime;
            }
        }
    }
}
