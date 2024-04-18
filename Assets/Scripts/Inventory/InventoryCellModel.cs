using System.Diagnostics;

public struct InventoryCellModel
{
    private int quantity;
    private ItemSO item;

    public int Quantity { get { return quantity; } }
    public ItemSO Item { get { return item; } }

    public bool IsEmpty => item == null;

    public int TryPutItems(ItemSO newItem, int quantity)
    {
        if (IsEmpty)
        {
            int newQuantity = quantity <= newItem.maxPerStack ? quantity : newItem.maxPerStack;
            PutItem(newItem, newQuantity);
            return quantity - newQuantity;
        }

        if (item.Title == newItem.Title)
        {
            // Calculate new quantity
            int newQuantity = this.quantity + quantity;

            // Check if there enough room for all items
            if (newQuantity <= newItem.maxPerStack)
            {
                // If yes - set new quantity and return 0
                this.quantity = newQuantity;
                return 0;
            }
            else
            {
                // If no - calculate how much items left and return it
                int left = newQuantity - newItem.maxPerStack;
                this.quantity = newItem.maxPerStack;
                return left;
            }
        }

        return quantity;
    }

    public void PutItem(ItemSO newItem, int newQuantity)
    {
        item = newItem;
        quantity = newQuantity;
    }

    public static InventoryCellModel GetEmptyCell()
    {
        return new InventoryCellModel
        {
            quantity = 0,
            item = null
        };
    }
}
