using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public ItemSO item;
    [SerializeField] private Image itemImage;

    private void Awake()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
        if (item != null && item.evolution >= item.dynamicImages.Count - 1 && !item.isCompleted)
            item.isCompleted = true;

    }

    public void UpdateUI()
    {
        if (item != null && item.evolution > -1)
        {
            itemImage.enabled = true;
            if (!item.dynamic)
                itemImage.sprite = item.image;
            else
                itemImage.sprite = item.dynamicImages[item.evolution];
        }
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
    }
}
