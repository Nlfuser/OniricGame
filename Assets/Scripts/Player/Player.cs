using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : movementSpeed;
        _rb.velocity = new Vector2(h * speed, _rb.velocity.y);
    }
}
