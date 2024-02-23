using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Room1 : MonoBehaviour
{
    [SerializeField] private List<ItemSO> itemsNeeded;
    [SerializeField] private Player player;
    [SerializeField] private SpriteRenderer bedRenderer;
    [SerializeField] private SpriteRenderer windowRenderer;
    [SerializeField] private SpriteRenderer bgRenderer;
    [SerializeField] private GameObject doorUnlocked;
    [SerializeField] private GameObject fallenFrame;
    [SerializeField] private GameObject fixedFrameFrame;
    [SerializeField] private Sprite bedBright;
    [SerializeField] private Sprite windowBright;
    [SerializeField] private Sprite bgBright;
    [SerializeField] private DialogueBubble dialogue;
    [SerializeField] private GameObject puzzle;
    private bool _started = false;
    private bool _startedPieces = false;
    private int _count;
    private List<ItemSO> _itemsCollected = new List<ItemSO>();

    private void Update()
    {
        if (!_started)
        {
            foreach (var item in itemsNeeded)
            {
                if (GameManager.instance.InventoryContains(item) && !_itemsCollected.Contains(item))
                {
                    _count++;
                    _itemsCollected.Add(item);
                }

                if (_count == 4)
                {
                    foreach (var item2 in _itemsCollected)
                    {
                        GameManager.instance.RemoveFromInventory(item2);
                    }                    
                    dialogue.StartDialogue();
                    player.SetCanMove(false);
                    _started = true;
                }
            }
        }

        if (GameManager.instance.GetPuzzlePieces() == 4 && !_startedPieces)
        {
            OnPuzzleComplete();
            _startedPieces = true;
        }
    }

    public void ShowPuzzle()
    {
        ((RectTransform)puzzle.transform).DOAnchorPosY(0f, 0.75f).SetEase(Ease.OutBack);
    }

    public void OnPuzzleComplete()
    {
        bedRenderer.sprite = bedBright;
        windowRenderer.sprite = windowBright;
        bgRenderer.sprite = bgBright;
        doorUnlocked.SetActive(true);
        fixedFrameFrame.SetActive(true);
        fallenFrame.SetActive(false);
        player.SetCanMove(true);
        ((RectTransform)puzzle.transform).DOAnchorPosY(1100f, 0.75f).SetEase(Ease.InBack);
        dialogue.transform.GetChild(0).gameObject.SetActive(false);
    }
}
