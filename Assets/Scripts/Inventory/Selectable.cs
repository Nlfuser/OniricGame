using System;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    private bool _isMouseOver;

    private void OnMouseEnter()
    {
        _isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
    }

    public bool IsPlayerClicking()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }
    
    public bool IsMouseOver()
    {
        return _isMouseOver;
    }
}