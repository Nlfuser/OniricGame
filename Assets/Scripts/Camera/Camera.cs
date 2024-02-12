using System;
using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player player;
    private CinemachineVirtualCamera _cam;

     private void Awake()
     {
         _cam = GetComponent<CinemachineVirtualCamera>();
     }

     private void Update()
     {
         if (player.GetDir() > 0)
             _cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset =
                 new Vector3(6, 3.25f, 0);
         else if (player.GetDir() < 0)
         {
             _cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset =
                 new Vector3(-6, 3.25f, 0);
         }
     }
}
