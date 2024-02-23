using System;
using TMPro;
using UnityEngine;

public class ItemCounter : Singleton<ItemCounter>
{
    [SerializeField] private int totalItemCount;
    [SerializeField] private TMP_Text totalItems;
    [SerializeField] private TMP_Text currentItems;
    private int _itemsPickedUp;

    private void Start()
    {
        totalItems.text = totalItemCount.ToString();
    }
    

    public void UpdateObjective()
    {
        _itemsPickedUp++;
        currentItems.text = _itemsPickedUp.ToString();
    }
}
