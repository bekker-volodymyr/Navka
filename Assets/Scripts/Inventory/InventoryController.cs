using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryModelSO inventoryModel;
    [SerializeField] private UIInventoryManager inventoryView;
    [SerializeField] private GameObject defaultItemPrefab;

    private int selectedItemIndex = -1;

    private void Start()
    {
        inventoryModel.Init();
        inventoryModel.OnInventoryUpdated += UpdateInventoryUI;
        Item.OnPickUp += AddItem;
        inventoryView.Init(inventoryModel.InventorySize);
        inventoryView.SelectItem += OnItemSelected;
        inventoryView.DeselectItemByClick += OnItemDeselected;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropSelectedItem();
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryCellModel> inventoryState)
    {
        inventoryView.ResetInventory();
        foreach (var item in inventoryState)
        {
            inventoryView.UpdateItemInInventory(item.Key, item.Value.Item.Sprite, item.Value.Quantity);
        }

        if (selectedItemIndex != -1)
        {
            selectedItemIndex = inventoryView.TrySelectItem(selectedItemIndex) ? selectedItemIndex : -1;
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {
        int left = inventoryModel.AddItem(item, quantity);
        if (left > 0)
        {
            Debug.Log("inventory full");
            GameObject leftItem = Instantiate(defaultItemPrefab);
            leftItem.GetComponent<Item>().InitItem(item, quantity);
        }
    }

    private void OnItemSelected(int selectedIndex)
    {
        selectedItemIndex = selectedIndex;
    }

    private void OnItemDeselected()
    {
        selectedItemIndex = -1;
    }

    private void DropSelectedItem()
    {
        if(selectedItemIndex != -1)
        {
            ItemSO droppedItem = inventoryModel.InventoryCells[selectedItemIndex].Item;
            inventoryModel.DropItem(selectedItemIndex);
            Instantiate(defaultItemPrefab).GetComponent<Item>().InitItem(droppedItem, 1);
        }
    }
}
