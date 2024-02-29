using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlacementArea : Interactable
{
    public bool scene2;
    public GameObject Placement;
    public bool shouldUpdateObjective;
    
    private void Start()
    {
        scene2 = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Room2";
    }
    public override void OnInteract()
    {
        InventoryUI.instance.Place(Placement);

        if (scene2)
        {
            GameManager.instance.IncreaseChatCount(); //This is bad alr?
            ItemCounter.instance.UpdateObjective();
        }
        
        if(shouldUpdateObjective)
            ItemCounter.instance.UpdateObjective();

        GameManager.instance.RemoveFromInventory(itemNeeded);

    }
}
