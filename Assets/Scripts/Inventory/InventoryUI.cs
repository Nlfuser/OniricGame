using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    public Action OnPlace;
    [SerializeField] private List<UISlot> uiSlots;
    [SerializeField] private Image holdingItemImage;
    private int _currentlySelectedItemIndex;
    private ItemSO _currentlySelectedItem;
    private bool _canPlace;
    private float _canPlaceTimer;

    private void Update()
    {
        _currentlySelectedItem = uiSlots[_currentlySelectedItemIndex].item;
        UpdateHoldingImage();

        UpdateSelectedItem();

        UpdatePlacingBool();
        
        foreach (var slot in uiSlots)
        {
            if (uiSlots[_currentlySelectedItemIndex] == slot)
                slot.Select();
            else
                slot.Deselect();
        }

        if (_currentlySelectedItem != null && _currentlySelectedItem.isNote && Input.GetKeyDown(KeyCode.E))
            NoteUI.instance.Show();
    }

    private void UpdatePlacingBool()
    {
        if (!_canPlace)
        {
            _canPlaceTimer += Time.deltaTime;
            if (_canPlaceTimer >= 0.25f)
                _canPlace = true;
        }

        if (_currentlySelectedItem)
            _canPlace = !_currentlySelectedItem.cantPlace && _canPlace;
        else
            _canPlace = false;
    }

    private void UpdateSelectedItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _currentlySelectedItemIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _currentlySelectedItemIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _currentlySelectedItemIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _currentlySelectedItemIndex = 3;
        if (Input.mouseScrollDelta.y < 0)
        {
            _currentlySelectedItemIndex++;
            if (_currentlySelectedItemIndex > 3)
                _currentlySelectedItemIndex = 0;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            _currentlySelectedItemIndex--;
            if (_currentlySelectedItemIndex < 0)
                _currentlySelectedItemIndex = 3;
        }
    }

    private void UpdateHoldingImage()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, UnityEngine.Camera.main, out var localPoint);
        holdingItemImage.transform.localPosition = localPoint;
        if (_currentlySelectedItem != null && !_currentlySelectedItem.cantPlace)
        {
            if (!_currentlySelectedItem.dynamic || uiSlots[_currentlySelectedItemIndex].item.isCompleted)
            {
                holdingItemImage.enabled = true;
                if(!_currentlySelectedItem.dynamic)
                    holdingItemImage.sprite = _currentlySelectedItem.image;
                else
                    holdingItemImage.sprite = _currentlySelectedItem.dynamicImages[_currentlySelectedItem.evolution];
            }
        }
        else
            holdingItemImage.enabled = false;
    }

    public void Place(GameObject pos = null)
    {
        if (_canPlace)
        {
            if (!_currentlySelectedItem.dynamic || _currentlySelectedItem.isCompleted)
            {
                var item = Instantiate(_currentlySelectedItem.placedPrefab);
                OnPlace?.Invoke();
                if (pos == null)
                {
                    var position = holdingItemImage.transform.position;
                    item.transform.position = new Vector3(position.x,
                        position.y, item.transform.position.z);
                }
                else
                {
                    var position = pos.transform.position;
                    item.transform.position = new Vector3(position.x,
                        position.y, item.transform.position.z);
                }

                GameManager.instance.RemoveFromInventory(_currentlySelectedItem);
            }
        }
    }

    public void AddItem(ItemSO item)
    {
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

    public void RemoveItem(ItemSO item)
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
    
    public ItemSO GetCurrentItem()
    {
        return _currentlySelectedItem;
    }
    
    public void SetCannotPlace()
    {
        _canPlace = false;
        _canPlaceTimer = 0f;
    }
}