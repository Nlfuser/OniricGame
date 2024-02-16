public abstract class Interactable : Selectable
{
    public ItemSO itemNeeded;
    
    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking() && GameManager.instance.InventoryContains(itemNeeded))
            OnInteract();
    }

    public abstract void OnInteract();
}
