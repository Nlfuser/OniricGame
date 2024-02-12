using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runSpeed;
    private Rigidbody2D _rb;

    private List<GameObject> Items = new List<GameObject>();
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Debug.Log("Open the door");
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            Debug.Log("Get the key");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Items.Add(other.gameObject);
                Debug.Log("Item got");
                Destroy(other.gameObject);
            }
        }
}
