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

    public void AddItem(ItemSO item, int quantity)
    {
        int left = quantity;

        for (int i = 0; i < ToolsBarSize; i++)
        {
            left = toolsBarCells[i].TryPutItems(item, left);
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
    }
}
