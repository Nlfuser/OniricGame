using System;

public class Item : Selectable
{
    private void Awake()
    {
        
    }

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking())
        {
            GameManager.instance.AddToInventory(this);
            Destroy(gameObject);
        }
    }
}
