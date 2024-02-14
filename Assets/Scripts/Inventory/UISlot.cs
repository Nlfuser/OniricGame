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
            itemImage.sprite = item.image;
            itemImage.enabled = true;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
    }
}
