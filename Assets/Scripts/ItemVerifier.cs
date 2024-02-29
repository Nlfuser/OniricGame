using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVerifier : MonoBehaviour
{
    
    public List<ItemSO> ItemSOList = new List<ItemSO>();
    private int count = 0;
    public GameObject MC;
    public GameObject door;
    void Update()
    {
        if(count == ItemSOList.Count){
            MC.SetActive(true);
            if(door)
                door.SetActive(true);
        }
    }

    private void Start()
    {
        InventoryUI.instance.OnPlace += OnPlaced;
    }

    private void OnDisable()
    {
        InventoryUI.instance.OnPlace -= OnPlaced;
    }

    private void OnPlaced(ItemSO itemSo){
        count+=1;
    }
}
