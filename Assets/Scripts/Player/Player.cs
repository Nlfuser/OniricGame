using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float runSpeed;
    private Rigidbody2D _rb;
    private Animator _animator;

    public enum playerState
    {
        IdleNormal,
        IdleTruePath,
        IdleSad,
        Walk,
        Run
    }

    public playerState currentState;

    //define reference time-variable
    private float lastStateChange = 0.0f;


    void Start()
    {
        _animator = GetComponent<Animator>();
        currentState = playerState.IdleNormal;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : movementSpeed;
        _rb.velocity = new Vector2(h * speed * Time.deltaTime * 250, _rb.velocity.y);
        switch (currentState)
        {
            case playerState.IdleNormal:
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
                break;
            case playerState.IdleTruePath:
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
                break;
            case playerState.IdleSad:
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
                break;
            case playerState.Walk:
                _animator.SetBool("IsWalking", true);
                _animator.SetBool("IsRunning", false);
                break;
            case playerState.Run:
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", true);
                break;
            default:
                break;
        }
    }

    public int GetDir()
    {
        Vector3 playerDirection = transform.forward;
        int directionX = Mathf.RoundToInt(playerDirection.x);
        return directionX;
    }

    public bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public void GoodDecision(){
        TransitionToState(playerState.IdleSad);
    }
    public void BadDecision(){
        TransitionToState(playerState.IdleTruePath);
    }

    public bool StartedRunning()
    {
        TransitionToState(playerState.Run);
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool EndedRunning()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }

    public void TransitionToState(playerState newState)
    {
        currentState = newState;
    }

    public playerState getCurrentState(){
        return currentState;
    }

}
