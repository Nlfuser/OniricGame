using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ItemDetector : Selectable
{
    String Name {get; set;}
    Sprite Icon {get; set;}
    public ItemDetector(String Name, Sprite Icon){
        this.Name = Name;
        this.Icon = Icon;
    }
    public GameObject[] Inventory;

    //public GameObject Door;
    public void Start(){
        Inventory = GameObject.FindGameObjectsWithTag("InventorySlot");
        //Door = GameObject.Find("Door");
    }
    private void Awake()
    {
        
    }

    private void Update()
    {
        Vector2 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            UnityEngine.Debug.Log("Door Detected");

            foreach (GameObject item in Inventory)
            {
                if (item.CompareTag("Key") && Input.GetMouseButtonDown(0))
                {
                    UnityEngine.Debug.Log("Door Opened");
                    break; 
                }
            }
        }
    }


    void AddItem(GameObject item)
    {
    foreach (GameObject InventorySlot in Inventory)
    {
        SpriteRenderer slotSpriteRenderer = InventorySlot.GetComponent<SpriteRenderer>();

        slotSpriteRenderer.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }//The sprite is added to multiple inventory slots instea of one.
}

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Item"))
        {
            AddItem(gameObject);
            Destroy(gameObject);
        }
    }
    

}
