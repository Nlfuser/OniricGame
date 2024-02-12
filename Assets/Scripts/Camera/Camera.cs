using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float xCamera;

    private void Update()
    {
        var playerPos = player.position;
        var cameraPos = transform.position;
        if (playerPos.x < cameraPos.x - xCamera)
            transform.position = cameraPos + Vector3.left * xCamera * 2;
        else if (playerPos.x > cameraPos.x + xCamera)
            transform.position = cameraPos + Vector3.right * xCamera * 2;
    }
}
