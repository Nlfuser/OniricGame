using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5 : MonoBehaviour
{
    [SerializeField] private RotateSprite pic; 
    private int _count;
    
    private void Update()
    {
        if(_count == 2 && pic && pic.isCompleted)
            SceneManager.instance.StartTransition("End Scene");
    }

    private void Start()
    {
        InventoryUI.instance.OnPlace += OnPlaced;
    }

    private void OnDisable()
    {
        InventoryUI.instance.OnPlace -= OnPlaced;
    }

    private void OnPlaced(ItemSO itemSo)
    {
        _count++;
        pic = FindObjectOfType<RotateSprite>();
    }
}
