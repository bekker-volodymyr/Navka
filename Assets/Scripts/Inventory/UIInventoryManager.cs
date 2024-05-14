using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject content;

    [SerializeField] private UIInventoryCell cellPrefab;

    [SerializeField] private GameObject inventoryCellsParent;

    public event Action<int> SelectItem;
    public event Action DeselectItemByClick;

    private List<UIInventoryCell> inventoryCells = new List<UIInventoryCell>();

    public void Init(int inventorySize)
    {
        InitCells(inventoryCells, inventoryCellsParent, inventorySize);
    }

    private void InitCells(List<UIInventoryCell> cells, GameObject cellsParent, int size)
    {
        for (int i = 0; i < size; i++)
        {
            cells.Add(InitCell(cellsParent));
        }
    }

    private UIInventoryCell InitCell(GameObject parent)
    {
        UIInventoryCell cell = Instantiate(cellPrefab);
        cell.ItemSelected += OnSelectItem;
        cell.ItemDeselectByClick += DeselectItem;
        cell.transform.SetParent(parent.transform, false);
        return cell;
    }

    public void ResetInventory()
    {
        foreach (UIInventoryCell cell in inventoryCells)
            cell.SetEmpty();
    }

    public void UpdateItemInInventory(int index, Sprite sprite, int quantity)
    {
        if (inventoryCells.Count > index)
        {
            inventoryCells[index].SetItem(sprite, quantity);
        }
    }

    public void Show()
    {
        content.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        content.gameObject.SetActive(false);
    }

    private void DeselectAll()
    {
        for(int i = 0; i < inventoryCells.Count; i++)
        {
            inventoryCells[i].Deselect();
        }
    }

    private void OnSelectItem(UIInventoryCell selectedCell)
    {
        DeselectAll();

        int selectedIndex = inventoryCells.IndexOf(selectedCell);

        SelectItem?.Invoke(selectedIndex);
    }

    private void DeselectItem(UIInventoryCell deselectedCell)
    {
        DeselectItemByClick?.Invoke();
    }

    public bool TrySelectItem(int index)
    {
        if(inventoryCells.Count > index)
        {
            return inventoryCells[index].TrySelect();
        }
        return false;
    }
}
