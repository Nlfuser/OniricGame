using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Item : Selectable
{
    public GameObject[] InventorySlots;
    //public GameObject Door;
    public void Start(){
        InventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        //Door = GameObject.Find("Door");
    }
    private void Awake()
    {
        
    }

    private void Update()
    {
    }

    private void OnMouseEnter()
    {
        // Collider2D DoorColider = Door.GetComponent<Collider2D>();
        // bool isTrigger = DoorColider.isTrigger;
        // if(isTrigger){

        // }

    }


    private void OnMouseDown(){
        Destroy(gameObject);
    }

    private void OnMouseUp(){
        ChangeSprite();
    }
    void ChangeSprite()
{
    foreach (GameObject InventorySlot in InventorySlots)
    {
        SpriteRenderer slotSpriteRenderer = InventorySlot.GetComponent<SpriteRenderer>();

        if (slotSpriteRenderer != null)
        {
            InventorySlot.GetComponent<Image>().overrideSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            
        }
    }
}


}
