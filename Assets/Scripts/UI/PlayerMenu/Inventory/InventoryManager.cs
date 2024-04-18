using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<InventoryCell> cells;
    private List<InventoryCell> toolsBar;
    [SerializeField] private int cellsCount = 16;
    [SerializeField] private int toolsBarCount = 5;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject cellsParent;

    [SerializeField] private GameObject toolsBarParentInGame;
    [SerializeField] private GameObject toolsBarParentInMenu;

    public void Initialize()
    {
        if (toolsBar is null)
        {
            InitToolsBar();
        }

        if (cells is null)
        {
            InitCells();
        }
    }

    private void InitToolsBar()
    {
        toolsBar = new List<InventoryCell>();
        for(int i = 0; i < toolsBarCount; i++) 
        {
            toolsBar.Add(InitCell(toolsBarParentInGame));
        }
    }

    private void InitCells()
    {
        cells = new List<InventoryCell>();

        for (int i = 0; i < cellsCount; i++)
        {
            cells.Add(InitCell(cellsParent));
        }
    }

    public void AddItem(ItemSO item, int count)
    {
        if(cells is null)
        {
            InitCells();
        }

        int left = count;

        for (int i = 0; i < cellsCount; i++)
        {
            left = cells[i].TryPutItems(item, left);

            if (left == 0)
            {
                break;
            }
        }
    }

    private InventoryCell InitCell(GameObject parent)
    {
        GameObject cell = Instantiate(cellPrefab);
        cell.transform.SetParent(parent.transform, false);
        return cell.GetComponent<InventoryCell>();
    }

    private void OnEnable()
    {
        for(int i = 0; i < toolsBar.Count; i++)
        {
            toolsBar[i].gameObject.transform.SetParent(toolsBarParentInMenu.transform, false);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < toolsBar.Count; i++)
        {
            toolsBar[i].gameObject.transform.SetParent(toolsBarParentInGame.transform, false);
        }
    }
}
