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

    [SerializeField] private GameObject toolsBarParentInGame;
    [SerializeField] private GameObject toolsBarParentInMenu;

    private List<UIInventoryCell> inventoryCells = new List<UIInventoryCell>();
    private List<UIInventoryCell> toolbarsCells = new List<UIInventoryCell>();

    public void Init(int inventorySize, int toolsBarSize)
    {
        InitCells(inventoryCells, inventoryCellsParent, inventorySize);
        InitCells(toolbarsCells, toolsBarParentInGame, toolsBarSize);
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
        cell.transform.SetParent(parent.transform, false);
        cell.SetEmpty();
        return cell;
    }

    public void UpdateToolsBar(List<InventoryCellModel> toolsBarItems)
    {
        for(int i = 0; i < toolbarsCells.Count; i++)
        {
            if (toolsBarItems[i].IsEmpty)
            {
                toolbarsCells[i].SetEmpty();
            }
            else
            {
                toolbarsCells[i].SetItem(toolsBarItems[i].Item, toolsBarItems[i].Quantity);
            }
        }
    }
}
