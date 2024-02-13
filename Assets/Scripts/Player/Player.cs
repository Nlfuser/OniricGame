using System;
using UnityEngine;

public enum PlayerGameState
{
    Load,
    Idle,
    Walk,
    Run,
    Stop
}

public class Player : MonoBehaviour
{
    public PlayerGameState stateOfPlayer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runSpeed;
    private Rigidbody2D _rb;
    private float _horizontal;
    private bool _isSprinting;
    private float _speed;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void SwitchPlayerState(PlayerGameState playerState)
    {
        switch (playerState)
        {
            case PlayerGameState.Load:
                break;
            case PlayerGameState.Idle:
                break;
            case PlayerGameState.Walk:
                _speed = movementSpeed;
                MovePlayer();
                break;
            case PlayerGameState.Run:
                _speed = runSpeed;
                MovePlayer();
                break;
            case PlayerGameState.Stop:
                break;
        }
        stateOfPlayer = playerState;
    }
    
    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (_horizontal != 0 && !_isSprinting)
            SwitchPlayerState(PlayerGameState.Walk);
        else if(_horizontal != 0 && _isSprinting)
            SwitchPlayerState(PlayerGameState.Run);
        if(_horizontal == 0)
            SwitchPlayerState(PlayerGameState.Idle);
    }

    private void MovePlayer()
    {
        _rb.velocity = new Vector2(_horizontal * _speed * Time.deltaTime * 250, _rb.velocity.y);
    }
    
    public float GetDir()
    {
        return _horizontal;
    }
    
    public bool StartedRunning()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
    
    public bool EndedRunning()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }
}
