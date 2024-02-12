using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(h * movementSpeed, _rb.velocity.y);
    }
}
