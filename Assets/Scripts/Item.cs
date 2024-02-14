using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Item : Selectable
{
    public GameObject[] IntervorySlots;
    //public GameObject Door;
    public void Start(){
        IntervorySlots = GameObject.FindGameObjectsWithTag("IntervorySlot");
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
    foreach (GameObject IntervorySlot in IntervorySlots)
    {
        SpriteRenderer slotSpriteRenderer = IntervorySlot.GetComponent<SpriteRenderer>();

        if (slotSpriteRenderer != null)
        {
            IntervorySlot.GetComponent<Image>().overrideSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            UnityEngine.Debug.LogWarning("SpriteRenderer component not found on IntervorySlot: " + IntervorySlot.name);
        }
    }
}


}
