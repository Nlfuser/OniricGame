using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.instance.StartTransition("Room1");
            print("a");
        }
    }
}
