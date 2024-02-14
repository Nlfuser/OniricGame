using System;
using UnityEngine;

public class Item : Selectable
{
    public ItemSO item;
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking())
        {
            GameManager.instance.AddToInventory(item);
            Destroy(gameObject);
        }
    }
}
