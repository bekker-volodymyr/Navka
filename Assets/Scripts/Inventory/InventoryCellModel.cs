public class InventoryCellModel
{
    private int quantity;
    private ItemSO item;

    public int Quantity { get { return quantity; } }
    public ItemSO Item { get { return item; } }

    public bool IsEmpty => item == null;

    public int TryPutItems(ItemSO newItem, int quantity)
    {
        // If cell is empty - calculate quantity and put an item
        if (IsEmpty)
        { 
            // If quantity less or equal max quantity in stack - new quantity is quantitiy, else - new quantity is max per stack
            int newQuantity = quantity <= newItem.maxPerStack ? quantity : newItem.maxPerStack;
            // Put item to cell
            PutItem(newItem, newQuantity);
            // Return left
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
    
    public void ReduceQuantity(int quantity)
    {
        this.quantity -= quantity;
        if(this.quantity <= 0) 
        {
            item = null;
            this.quantity = 0;
        }
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
