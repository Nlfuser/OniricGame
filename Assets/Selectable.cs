using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool IsPlayerClicking()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }
}