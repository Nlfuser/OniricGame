using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlacementArea : Interactable
{
    public bool scene2;
    public GameObject Placement;
    private void Start()
    {
        scene2 = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Room2";
    }
    public override void OnInteract(){


        InventoryUI.instance.Place(Placement);

        if(scene2) GameManager.instance.IncreaseChatCount(); //This is bad alr?

        GameManager.instance.RemoveFromInventory(itemNeeded);

    }

}
