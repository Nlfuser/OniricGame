using System.Collections.Generic;

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

    private List<ItemSO> _inventory = new List<ItemSO>();

    public void SetGameState(GameState state)
    {
        _gameState = state;
    }

    public void AddToInventory(ItemSO obj)
    {
        if (!obj.dynamic || !_inventory.Contains(obj))
            _inventory.Add(obj);
        InventoryUI.instance.UpdateUI(obj);
    }
    
    public void RemoveFromInventory(ItemSO obj)
    {
        _inventory.Remove(obj);
        InventoryUI.instance.RemoveUI();
    }

    public bool InventoryContains(ItemSO obj)
    {
        return _inventory.Contains(obj);
    }
}
