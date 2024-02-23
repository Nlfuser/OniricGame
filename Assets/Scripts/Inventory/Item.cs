using System;
using UnityEngine;

public class Item : Selectable
{
    public ItemSO item;
    public bool shouldUpdateObjective;

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking() && !GameManager.instance.InventoryFull())
        {
            GameManager.instance.AddToInventory(item);
            if (item.isCompleted)
                item.evolution = item.dynamicImages.Count - 1;
            if (item.isNote)
            {
                GameManager.instance.AddNote();
                ItemCounter.instance.UpdateObjective();
            }
            if(shouldUpdateObjective && !item.isNote)
                ItemCounter.instance.UpdateObjective();

            InventoryUI.instance.SetIsPickupUpTrue();
            Destroy(gameObject);
        }
    }
}
