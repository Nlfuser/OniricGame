using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlacementArea : Interactable
{

    public GameObject Placement;

    public override void OnInteract(){


        InventoryUI.instance.Place(Placement);

        GameManager.instance.RemoveFromInventory(itemNeeded);

    }

}
