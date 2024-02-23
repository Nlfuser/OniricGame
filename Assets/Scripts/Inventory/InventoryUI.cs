using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    public Action OnPlace;
    public Action OnPickup;
    [SerializeField] private List<UISlot> uiSlots;
    [SerializeField] private Image holdingItemImage;
    private int _currentlySelectedItem;
    private bool _isPickingUp;
    private float _pickupTimer;
    private List<ItemSO> _itemCounter = new List<ItemSO>();

    public ItemSO GetCurrentItem()
    {
        return uiSlots[_currentlySelectedItem].item;
    }
    
    public void SetIsPickupUpTrue()
    {
        _isPickingUp = true;
        _pickupTimer = 0f;
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, UnityEngine.Camera.main, out var localPoint);
        holdingItemImage.transform.localPosition = localPoint;
        if (uiSlots[_currentlySelectedItem].item != null && !uiSlots[_currentlySelectedItem].item.cantPlace)
        {
            if (!uiSlots[_currentlySelectedItem].item.dynamic ||
                (uiSlots[_currentlySelectedItem].item.dynamic && uiSlots[_currentlySelectedItem].item.isCompleted))
            {
                holdingItemImage.enabled = true;
                if(!uiSlots[_currentlySelectedItem].item.dynamic)
                    holdingItemImage.sprite = uiSlots[_currentlySelectedItem].item.image;
                else
                    holdingItemImage.sprite = uiSlots[_currentlySelectedItem].item.dynamicImages[uiSlots[_currentlySelectedItem].item.evolution];
            }
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
            if (uiSlots[_currentlySelectedItem] == slot)
            {
                if (uiSlots[_currentlySelectedItem].transform.localScale.x != 1.25f)
                    slot.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.35f);
            }
            else
                if(slot.transform.localScale.x != 1f)
                    slot.transform.DOScale(new Vector3(1f, 1f, 1f), 0.35f);
        }

        if (_isPickingUp)
        {
            _pickupTimer += Time.deltaTime;
            if (_pickupTimer >= 0.25f)
                _isPickingUp = false;
        }

        if (uiSlots[_currentlySelectedItem].item != null && uiSlots[_currentlySelectedItem].item.isNote && Input.GetKeyDown(KeyCode.E))
            NoteUI.instance.Show();
    }

    public void Place(GameObject pos = null)
    {
        if (!_isPickingUp)
        {
            if (uiSlots[_currentlySelectedItem].item != null)
            {
                if (!uiSlots[_currentlySelectedItem].item.cantPlace)
                {
                    if (!uiSlots[_currentlySelectedItem].item.dynamic ||
                        (uiSlots[_currentlySelectedItem].item.dynamic && uiSlots[_currentlySelectedItem].item.isCompleted))
                    {
                        var item = Instantiate(uiSlots[_currentlySelectedItem].item.placedPrefab);
                        OnPlace?.Invoke();
                        _itemCounter.Add(uiSlots[_currentlySelectedItem].item);
                        if (pos == null)
                            item.transform.position = new Vector3(holdingItemImage.transform.position.x,
                                holdingItemImage.transform.position.y, 0f);
                        else
                            item.transform.position = new Vector3(pos.transform.position.x,
                                pos.transform.position.y, 0f);

                        GameManager.instance.RemoveFromInventory(uiSlots[_currentlySelectedItem].item);
                    }
                }
            }
        }
    }

    public bool PlacedContains(ItemSO item)
    {
        return _itemCounter.Contains(item);
    }
    
    public void AddUI(ItemSO item)
    {
        OnPickup?.Invoke();
        foreach (var slot in uiSlots)
        {
            if (!item.dynamic || item.evolution == -1)
            {
                if (slot.item != null)
                    continue;
                slot.item = item;
                slot.item.evolution++;
                break;
            }
            else if (item.dynamic && item.evolution != -1)
            {
                if (slot.item != item)
                    continue;
                slot.item.evolution++;
            }
        }
    }

    public void RemoveUI(ItemSO item)
    {
        for (var i = 0; i < uiSlots.Count; i++)
        {
            if (uiSlots[i].item == item)
            {
                uiSlots[i].item = null;
                uiSlots[i].UpdateUI();
            }
        }
    }
}