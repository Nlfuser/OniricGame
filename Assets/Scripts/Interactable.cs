public abstract class Interactable : Selectable
{
    public ItemSO itemNeeded;

    private void Update()
    {
        if(PauseMenu.instance.IsPaused())
            return;
        var canInteract = false;
        if (itemNeeded.dynamic)
            canInteract = InventoryUI.instance.GetCurrentItem() == itemNeeded && itemNeeded.isCompleted;
        else
            canInteract = InventoryUI.instance.GetCurrentItem() == itemNeeded;
        if (IsMouseOver() && IsPlayerClicking() && GameManager.instance.InventoryContains(itemNeeded) && canInteract)
            OnInteract();
    }

    public abstract void OnInteract();
}