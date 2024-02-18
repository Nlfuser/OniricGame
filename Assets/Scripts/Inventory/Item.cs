using System;
using UnityEngine;

public class Item : Selectable
{
    public ItemSO item;

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking() && !GameManager.instance.InventoryFull())
        {
            GameManager.instance.AddToInventory(item);
            if (item.isCompleted)
                item.evolution = item.dynamicImages.Count - 1;
            if(item.isNote)
                GameManager.instance.AddNote();
            InventoryUI.instance.SetIsPickupUpTrue();
            Destroy(gameObject);
        }
    }
}
