using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Interactable
{

    public GameObject Basket;
    public override void OnInteract()
    {

        GameManager.instance.RemoveFromInventory(itemNeeded);

        Basket.GetComponent<BoxCollider2D>().enabled = true;

    }
}
