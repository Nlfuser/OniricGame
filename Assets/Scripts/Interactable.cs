public abstract class Interactable : Selectable
{
    public ItemSO itemNeeded;

    private void Update()
    {
        if (IsMouseOver() && IsPlayerClicking() && GameManager.instance.InventoryContains(itemNeeded) && InventoryUI.instance.GetCurrentItem() == itemNeeded)
            OnInteract();
    }

    public abstract void OnInteract();
}