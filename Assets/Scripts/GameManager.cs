using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InRoom,
    InCinematic
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<ItemSO> allItems = new List<ItemSO>();
    [SerializeField] private GameObject ConvoObject;
    private DialogTrigger dialog;
    public GameState GameState => _gameState;
    private GameState _gameState;

    private List<ItemSO> _inventory = new List<ItemSO>();
    private int _acquiredNotes;
    private int _puzzlePiecesPlaced;

    protected override void Awake()
    {
        base.Awake();
        foreach (var item in allItems)
        {
            item.isCompleted = false;
            item.evolution = -1;
        }
    }

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) =>
        {
            try
            {
                dialog = GameObject.FindWithTag("DialogueCollider").GetComponent<DialogTrigger>();
                ConvoObject = GameObject.FindWithTag("DialogueCollider").gameObject;
            }
            catch { /*die*/ }
        };
    }

    public void SetGameState(GameState state)
    {
        _gameState = state;
    }

    public int GetNotes()
    {
        return _acquiredNotes;
    }
    
    public int GetPuzzlePieces()
    {
        return _puzzlePiecesPlaced;
    }
    
    public void AddNote()
    {
        _acquiredNotes++;
    }

    public void AddPuzzlePiece()
    {
        _puzzlePiecesPlaced++;
    }
    
    public void AddToInventory(ItemSO obj)
    {
        if (!obj.dynamic || !_inventory.Contains(obj))
        {
            obj.evolution = -1;
            _inventory.Add(obj);
        }

        InventoryUI.instance.UpdateUI(obj);
    }
    
    public void RemoveFromInventory(ItemSO obj)
    {
        _inventory.Remove(obj);
        InventoryUI.instance.RemoveUI(obj);
    }

    public bool InventoryContains(ItemSO obj)
    {
        return _inventory.Contains(obj);
    }

    public void IncreaseChatCount()
    {
        dialog.IncreaseChatIndex();
        dialog.ChatOneShot();
    }

    public bool InventoryFull()
    {
        return _inventory.Count >= 4;
    }
}
