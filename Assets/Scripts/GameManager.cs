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
    public GameState GameState => _gameState;
    private GameState _gameState;

    private List<Selectable> _inventory = new List<Selectable>();

    public void SetGameState(GameState state)
    {
        _gameState = state;
    }

    public void AddToInventory(Selectable obj)
    {
        _inventory.Add(obj);
    }

    public bool InventoryContains(Selectable obj)
    {
        return  _inventory.Contains(obj);
    }
}
