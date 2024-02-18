using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.instance.StartTransition("Room1");
    }
}
