using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public ItemSO item;
    [SerializeField] private Image itemImage;
    private Tweener _slotTweener;
    private Tweener _slotExitTweener;
    private bool _isSelected;

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

    public void Select()
    {
        if(_slotTweener == null && !_isSelected && gameObject.activeInHierarchy)
            transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.35f).OnComplete(() => _slotTweener = null).SetAutoKill(true);
        _isSelected = true;
    }

    public void Deselect()
    {
        if(_slotExitTweener == null && _isSelected && gameObject.activeInHierarchy)
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.35f).OnComplete(() => _slotExitTweener = null).SetAutoKill(true);
        _isSelected = false;
    }
}
