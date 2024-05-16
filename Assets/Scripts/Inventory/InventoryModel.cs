using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName="InventoryModelSO", menuName="Inventory/InventoryModelSO")]
public class InventoryModelSO : ScriptableObject
{
    private List<InventoryCellModel> inventoryCells;

    public List<InventoryCellModel> InventoryCells { get { return inventoryCells; } }

    [field: SerializeField] public int InventorySize { get; private set; }

    public event Action<Dictionary<int, InventoryCellModel>> OnInventoryUpdated;

    public void Init()
    {
        inventoryCells = new List<InventoryCellModel> ();

        for (int i = 0; i < InventorySize; i++)
        {
            inventoryCells.Add(InventoryCellModel.GetEmptyCell());
        }
    }

    public int AddItem(ItemSO item, int quantity)
    {
        int left = quantity;

        for (int i = 0; i < InventorySize; i++)
        {
            left = inventoryCells[i].TryPutItems(item, left);

            if (left == 0)
            {
                NotifyAboutChanges();
                return left;
            }

            if (IsInventoryFull())
            {
                NotifyAboutChanges();
                return left;
            }
        }

        NotifyAboutChanges();
        return left;
    }

    private bool IsInventoryFull()
            => InventoryCells.Where(item => item.IsEmpty).Any() == false;

    public Dictionary<int, InventoryCellModel> GetCurrentInventory()
    {
        Dictionary<int, InventoryCellModel> returnValue = new Dictionary<int, InventoryCellModel>();

        for (int i = 0; i < inventoryCells.Count; i++)
        {
            if (inventoryCells[i].IsEmpty)
                continue;

            returnValue[i] = inventoryCells[i];
        }
        return returnValue;
    }

    private void NotifyAboutChanges()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventory());
    }

    public void ReduceItemQuantity(int selectedIndex, int quantity)
    {
        if(selectedIndex < inventoryCells.Count)
        {
            if (!inventoryCells[selectedIndex].IsEmpty)
            {
                inventoryCells[selectedIndex].ReduceQuantity(quantity);
                NotifyAboutChanges();
            }
        }
    }

    public InventoryCellModel GetItemByIndex(int index)
    {
        if(index < inventoryCells.Count)
        {
            return inventoryCells[index];
        }
        return InventoryCellModel.GetEmptyCell();
    }
}
