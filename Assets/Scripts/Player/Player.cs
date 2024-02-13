using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runSpeed;
    private Rigidbody2D _rb;
    private float h;
    private bool s;
    private float speed;
    public enum PlayerGameState
    {
        load,
        idle,
        walk,
        run,
        stop
    }
    public PlayerGameState stateOfPlayer;
    public void PlayerStateMachine(PlayerGameState playerState)
    {
        switch (playerState)
        {
            case PlayerGameState.load:
                break;
            case PlayerGameState.idle:
                break;
            case PlayerGameState.walk:
                speed = movementSpeed;
                _rb.velocity = new Vector2(h * speed * Time.deltaTime * 250, _rb.velocity.y);
                break;
            case PlayerGameState.run:
                speed = runSpeed;
                _rb.velocity = new Vector2(h * speed * Time.deltaTime * 250, _rb.velocity.y);
                break;
            case PlayerGameState.stop:
                break;
        }
        stateOfPlayer = playerState;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }    
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        s = Input.GetKey(KeyCode.LeftShift);
        if (h != 0 && !s)
        {
            PlayerStateMachine(PlayerGameState.walk);
        }
        else if(h != 0 && s)
        {
            PlayerStateMachine(PlayerGameState.run);
        }
    }

    public float GetDir()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
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
