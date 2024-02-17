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
    public GameState GameState => _gameState;
    private GameState _gameState;

    private List<ItemSO> _inventory = new List<ItemSO>();

    protected override void Awake()
    {
        base.Awake();
        foreach (var item in allItems)
            item.isCompleted = false;
    }

    public void SetGameState(GameState state)
    {
        _gameState = state;
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
}
