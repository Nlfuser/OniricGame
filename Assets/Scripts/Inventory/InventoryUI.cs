using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] private List<UISlot> uiSlots;
    [SerializeField] private Image holdingItemImage;
    private int _currentlySelectedItem;

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, UnityEngine.Camera.main, out var localPoint);
        holdingItemImage.transform.localPosition = localPoint;
        if (uiSlots[_currentlySelectedItem].item != null)
        {
            holdingItemImage.enabled = true;
            holdingItemImage.sprite = uiSlots[_currentlySelectedItem].item.image;
        }
        else
            holdingItemImage.enabled = false;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _currentlySelectedItem = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _currentlySelectedItem = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _currentlySelectedItem = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _currentlySelectedItem = 3;
        if (Input.mouseScrollDelta.y < 0)
        {
            _currentlySelectedItem++;
            if (_currentlySelectedItem > 3)
                _currentlySelectedItem = 0;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            _currentlySelectedItem--;
            if (_currentlySelectedItem < 0)
                _currentlySelectedItem = 3;
        }

        foreach (var slot in uiSlots)
        {
            if(uiSlots[_currentlySelectedItem] == slot)
                slot.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.35f);
            else
                slot.transform.DOScale(new Vector3(1f, 1f, 1f), 0.35f);
        }
    }

    public void UpdateUI(ItemSO item)
    {
        foreach (var slot in uiSlots)
        {
            if(slot.item != null)
                continue;
            slot.item = item;
            slot.UpdateUI();
            break;
        }
    }
    
    public void RemoveUI()
    {
        for (var i = 0; i < uiSlots.Count; i++)
        {
            if (i == _currentlySelectedItem)
            {
                uiSlots[i].item = null;
                uiSlots[i].UpdateUI();
            }
        }
    }
}