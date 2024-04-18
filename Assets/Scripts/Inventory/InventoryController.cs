using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryModelSO inventoryModel;
    [SerializeField] private UIInventoryManager inventoryView;

    private void Start()
    {
        inventoryModel.Init();
        inventoryView.Init(inventoryModel.InventorySize, inventoryModel.ToolsBarSize);
    }

    public void AddItem(ItemSO item, int quantity)
    {
        inventoryModel.AddItem(item, quantity);
        inventoryView.UpdateToolsBar(inventoryModel.ToolsBarCells);
    }
}
