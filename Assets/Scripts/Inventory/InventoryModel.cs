using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="InventoryModelSO", menuName="Inventory/InventoryModelSO")]
public class InventoryModelSO : ScriptableObject
{
    private List<InventoryCellModel> inventoryCells;
    private List<InventoryCellModel> toolsBarCells;

    public List<InventoryCellModel> InventoryCells { get { return inventoryCells; } }
    public List<InventoryCellModel> ToolsBarCells { get { return toolsBarCells; } }

    [field: SerializeField] public int InventorySize { get; private set; }
    [field: SerializeField] public int ToolsBarSize { get; private set; }

    public event Action<Dictionary<int, InventoryCellModel>> OnInventoryUpdated;
    public event Action<Dictionary<int, InventoryCellModel>> OnToolsBarUpdated;

    public void Init()
    {
        Debug.Log("Model init");
        inventoryCells = new List<InventoryCellModel> ();

        for (int i = 0; i < InventorySize; i++)
        {
            inventoryCells.Add(InventoryCellModel.GetEmptyCell());
        }

        toolsBarCells = new List<InventoryCellModel> ();

        for(int i = 0; i < ToolsBarSize; i++)
        {
            toolsBarCells.Add(InventoryCellModel.GetEmptyCell());
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {
        Debug.Log($"Model {item.Title} {quantity}");

        int left = quantity;

        for (int i = 0; i < ToolsBarSize; i++)
        {
            Debug.Log(toolsBarCells[i].Item is null);
            left = toolsBarCells[i].TryPutItems(item, left);
            Debug.Log(left);
            Debug.Log(toolsBarCells[i].Item is null);
            if (left == 0)
            {
                return;
            }
        }

        for (int i = 0; i < InventorySize; i++)
        {
            left = inventoryCells[i].TryPutItems(item, left);

            if (left == 0)
            {
                return;
            }
        }

        NotifyAboutChanges();
    }

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

    public Dictionary<int, InventoryCellModel> GetCurrentToolsBar()
    {
        Dictionary<int, InventoryCellModel> returnValue = new Dictionary<int, InventoryCellModel>();

        for (int i = 0; i < toolsBarCells.Count; i++)
        {
            if (toolsBarCells[i].IsEmpty)
                continue;

            returnValue[i] = toolsBarCells[i];
        }
        return returnValue;
    }

    private void NotifyAboutChanges()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventory());
        OnToolsBarUpdated?.Invoke(GetCurrentToolsBar());
    }
}
