using UnityEngine;

public class Squirrel : Interactable
{
    public GameObject Basket;
    
    public override void OnInteract()
    {
        GameManager.instance.RemoveFromInventory(itemNeeded);
        ItemCounter.instance.UpdateObjective();

        Basket.GetComponent<BoxCollider2D>().enabled = true;
    }
}
