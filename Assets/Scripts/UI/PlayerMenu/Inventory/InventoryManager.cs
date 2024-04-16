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

    [SerializeField] private GameObject toolsBarParent;

    private void Awake()
    {
        if (toolsBar is null)
        {
            InitToolsBar();
        }
    }

    private void Start()
    {
        if(cells is null)
        {
            InitCells();
        }
    }

    private void InitToolsBar()
    {
        toolsBar = new List<InventoryCell>();
        for(int i = 0; i < toolsBarCount; i++) 
        {
            InitCell(toolsBarParent);
        }
    }

    private void InitCells()
    {
        cells = new List<InventoryCell>();

        for (int i = 0; i < cellsCount; i++)
        {
            InitCell(cellsParent);
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

    private void InitCell(GameObject parent)
    {
        GameObject cell = Instantiate(cellPrefab);
        cell.transform.SetParent(parent.transform, false);
        cells.Add(cell.GetComponent<InventoryCell>());
    }
}
