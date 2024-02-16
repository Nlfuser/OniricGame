using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float duration;
    [SerializeField] private float sprintDuration = 1f;
    [SerializeField] private Ease ease;
    private CinemachineVirtualCamera _cam;
    private float _previousPlayerDirection;
    private Tweener _resetTween;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        var framingTransposer = _cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (player.stateOfPlayer == PlayerGameState.Run)
        {
            if (_previousPlayerDirection != player.GetDir())
            {
                DOVirtual.Float(framingTransposer.m_TrackedObjectOffset.x, 6f * player.GetDir(), duration, value =>
                {
                    framingTransposer.m_TrackedObjectOffset = new Vector3(value, 2.5f, 0);
                }).SetEase(ease);
                _previousPlayerDirection = player.GetDir();
            }
        }
        
        if (player.stateOfPlayer is PlayerGameState.Walk or PlayerGameState.Idle && _resetTween == null)
        {
            _previousPlayerDirection = 99f;
            _resetTween = DOVirtual.Float(framingTransposer.m_TrackedObjectOffset.x, 0, duration, value =>
            {
                framingTransposer.m_TrackedObjectOffset = new Vector3(value, 2.5f, 0);
            }).SetEase(ease).OnComplete(() => _resetTween = null);
        }
    }

    private void LateUpdate()
    {
        //if (player.StartedRunning())
            //ChangeOrthographicSize(6f);
        //if (player.EndedRunning())
            //ChangeOrthographicSize(5f);
    }

    private void ChangeOrthographicSize(float targetSize)
    {
        DOVirtual.Float(_cam.m_Lens.OrthographicSize, targetSize, sprintDuration, value =>
        {
            _cam.m_Lens.OrthographicSize = value;
        }).SetEase(ease);
    
        _cam.m_Lens.OrthographicSize = targetSize;
    }
}
