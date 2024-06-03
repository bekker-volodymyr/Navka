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

    public event Action<ItemSO> ItemSelectedEvent;
    public event Action ItemDeselectedEvent;

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

        ItemSelectedEvent?.Invoke(inventoryModel.GetItemByIndex(selectedItemIndex).Item);
    }

    private void OnItemDeselected()
    {
        selectedItemIndex = -1;

        ItemDeselectedEvent?.Invoke();
    }

    private void DropSelectedItem()
    {
        if(selectedItemIndex != -1)
        {
            ItemSO droppedItem = inventoryModel.InventoryCells[selectedItemIndex].Item;
            inventoryModel.ReduceItemQuantity(selectedItemIndex, 1);
            Instantiate(defaultItemPrefab).GetComponent<Item>().InitItem(droppedItem, 1);
        }
    }

    public void ConsumeSelectedItem()
    {
        inventoryModel.ReduceItemQuantity(selectedItemIndex, 1);
    }

    public ItemSO GetSelectedItem()
    {
        if(selectedItemIndex == -1) return null;

        return inventoryModel.GetItemByIndex(selectedItemIndex).Item;
    }
}
