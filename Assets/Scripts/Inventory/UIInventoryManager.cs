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
    private List<UIInventoryCell> toolsBarCells = new List<UIInventoryCell>();

    public void Init(int inventorySize, int toolsBarSize)
    {
        InitCells(inventoryCells, inventoryCellsParent, inventorySize);
        InitCells(toolsBarCells, toolsBarParentInGame, toolsBarSize);
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
        return cell;
    }

    public void ResetInventory()
    {
        foreach (UIInventoryCell cell in inventoryCells)
            cell.SetEmpty();
    }

    public void ResetToolsBar()
    {
        foreach (UIInventoryCell cell in toolsBarCells)
            cell.SetEmpty();
    }

    public void UpdateItemInInventory(int index, Sprite sprite, int quantity)
    {
        if (inventoryCells.Count > index)
        {
            inventoryCells[index].SetItem(sprite, quantity);
        }
    }

    public void UpdateItemInToolsBar(int index, Sprite sprite, int quantity)
    {
        if (toolsBarCells.Count > index)
        {
            toolsBarCells[index].SetItem(sprite, quantity);
        }
    }

    public void Show()
    {
        content.gameObject.SetActive(true);
        ToggleToolsBar(true);
    }

    public void Hide()
    {
        content.gameObject.SetActive(false);
        ToggleToolsBar(false);
    }

    private void ToggleToolsBar(bool isMenu)
    {
        if(isMenu)
        {
            for (int i = 0; i < toolsBarCells.Count; i++)
            {
                toolsBarCells[i].gameObject.transform.SetParent(toolsBarParentInMenu.transform, false);
            }
        }
        else
        {
            for (int i = 0; i < toolsBarCells.Count; i++)
            {
                toolsBarCells[i].gameObject.transform.SetParent(toolsBarParentInGame.transform, false);
            }
        }
    }
}
