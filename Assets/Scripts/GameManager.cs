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

    public void SetGameState(GameState state)
    {
        _gameState = state;
    }
}
