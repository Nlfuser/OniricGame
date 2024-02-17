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
    [SerializeField] private bool small;
    private Rigidbody2D _rb;
    private float _horizontal;
    private bool _canMove;
    private bool _isSprinting;
    private float _speed;
    private Animator _anim;
    private string _currentAnimationName;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        SetCanMove(true);
    }
    
    public void SwitchPlayerState(PlayerGameState playerState)
    {
        switch (playerState)
        {
            case PlayerGameState.Load:
                break;
            case PlayerGameState.Idle:
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
                if (_currentAnimationName != "Idle")
                {
                    _anim.Play("Idle");
                    _currentAnimationName = "Idle";
                }
                break;
            case PlayerGameState.Walk:
                _speed = movementSpeed;
                MovePlayer();
                if (_currentAnimationName != "Walk")
                {
                    _anim.Play("Walk");
                    _currentAnimationName = "Walk";
                }

                if (!small)
                {
                    if (_horizontal > 0)
                        transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
                    else if (_horizontal < 0)
                        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }
                else
                {
                    if (_horizontal > 0)
                        transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
                    else if (_horizontal < 0)
                        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                }

                break;
            // case PlayerGameState.Run:
            //     _speed = runSpeed;
            //     MovePlayer();
            //     break;
            case PlayerGameState.Stop:
                break;
        }
        stateOfPlayer = playerState;
    }
    
    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        //_isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (_horizontal != 0 ) //Add &&_isSprinting back if we are readding sprint
            SwitchPlayerState(PlayerGameState.Walk);
        //else if(_horizontal != 0 && _isSprinting)
            //SwitchPlayerState(PlayerGameState.Run);
        if(_horizontal == 0)
            SwitchPlayerState(PlayerGameState.Idle);
    }

    private void MovePlayer()
    {
        if(!_canMove)
            return;
        _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
    }
    
    public float GetDir()
    {
        return _horizontal;
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }
    
    //public bool StartedRunning()
    //{
        //return Input.GetKeyDown(KeyCode.LeftShift);
    //}
                                                            //I dont see the purpose in having a run function
    // public bool EndedRunning()
    // {
    //     return Input.GetKeyUp(KeyCode.LeftShift);
    // }
}
