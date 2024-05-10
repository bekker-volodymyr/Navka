using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public int AddItem(ItemSO item, int quantity)
    {

        int left = quantity;

        for (int i = 0; i < ToolsBarSize; i++)
        {
            left = toolsBarCells[i].TryPutItems(item, left);

            if (left == 0)
            {
                NotifyAboutChanges();
                return left;
            }
        }

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

    public Dictionary<int, InventoryCellModel> GetCurrentToolsBar()
    {
        Dictionary<int, InventoryCellModel> returnValue = new Dictionary<int, InventoryCellModel>();

        for (int i = 0; i < toolsBarCells.Count; i++)
        {
            Debug.Log($"Class: InventoryModel --- Method: GetCurretnToolsBar --- ToolsBarCell: {toolsBarCells[i].Item?.Title}");

            if (toolsBarCells[i].IsEmpty)
                continue;

            returnValue[i] = toolsBarCells[i];
        }
        return returnValue;
    }

    private void NotifyAboutChanges()
    {
        Debug.Log($"Class: InventoryModel --- Method: NotifyAboutChanges --- CalledMethod: GetCurrentToolsBar --- Result: {GetCurrentToolsBar().Count}");

        OnInventoryUpdated?.Invoke(GetCurrentInventory());
        OnToolsBarUpdated?.Invoke(GetCurrentToolsBar());
    }
}
