using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runSpeed;
    private Rigidbody2D _rb;

    private List<GameObject> Items = new List<GameObject>();
    public Transform slotsParent; // Parent object of inventory slots
    public GameObject slotPrefab; // Prefab for inventory slots

    void Start(){
        for (int i = 0; i < Items.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            Image slotImage = slot.GetComponent<Image>();
            slotImage.sprite = Items[i].GetComponent<SpriteRenderer>().sprite;
        }

    }
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
                AddItem(other.gameObject);
                Debug.Log("Item got");
                Destroy(other.gameObject);
            }
        }
    }

    void AddItem(GameObject item)
    {
        Items.Add(item);
        GameObject slot = Instantiate(slotPrefab, slotsParent);
        Image slotImage = slot.GetComponent<Image>();
        slotImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public int GetDir(){
        Vector3 playerDirection = transform.forward;
        int directionX = Mathf.RoundToInt(playerDirection.x);
        return directionX;
    }
}
