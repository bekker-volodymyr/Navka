using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryModelSO inventoryModel;
    [SerializeField] private UIInventoryManager inventoryView;

    private void Start()
    {
        inventoryModel.Init();
        inventoryModel.OnInventoryUpdated += UpdateInventoryUI;
        inventoryModel.OnToolsBarUpdated += UpdateToolsBarUI;
        Item.OnPickUp += AddItem;
        inventoryView.Init(inventoryModel.InventorySize, inventoryModel.ToolsBarSize);
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryCellModel> inventoryState)
    {
        inventoryView.ResetInventory();
        foreach (var item in inventoryState)
        {
            inventoryView.UpdateItemInInventory(item.Key, item.Value.Item.Sprite, item.Value.Quantity);
        }
    }
    private void UpdateToolsBarUI(Dictionary<int, InventoryCellModel> toolsBarState)
    {
        inventoryView.ResetToolsBar();
        foreach (var item in toolsBarState)
        {
            inventoryView.UpdateItemInToolsBar(item.Key, item.Value.Item.Sprite, item.Value.Quantity);
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {
        Debug.Log($"Controller {item.Title} {quantity}");
        Debug.Log($"{inventoryModel is null}");
        Debug.Log($"{item is null}");
        inventoryModel.AddItem(item, quantity);
        UpdateInventoryUI(inventoryModel.GetCurrentInventory());
        UpdateToolsBarUI(inventoryModel.GetCurrentToolsBar());
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     if (inventoryView.isActiveAndEnabled == false)
        //     {
        //         inventoryView.Show();
        //         foreach (var item in inventoryModel.GetCurrentInventory())
        //         {
        //             inventoryView.UpdateItemInInventory(item.Key, item.Value.Item.Sprite, item.Value.Quantity);
        //         }
        //     }
        //     else
        //     {
        //         inventoryView.Hide();
        //     }
        // }
    }
}
