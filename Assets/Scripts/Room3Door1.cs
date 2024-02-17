using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3Door1 : Interactable
{

    public GameObject LockedDoor;
    public GameObject NormalDoor;
    public override void OnInteract()
    {

        GameManager.instance.RemoveFromInventory(itemNeeded);

        LockedDoor.SetActive(false);
        NormalDoor.SetActive(true);

    }
}
