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

    public void UpdateUI()
    {
        if (item != null)
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
