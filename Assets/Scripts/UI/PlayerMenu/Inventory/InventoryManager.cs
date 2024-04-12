using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<InventoryCell> cells;
    [SerializeField] private int cellsCount = 16;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject cellsParent;

    private void Start()
    {
        if(cells is null)
        {
            InitCells();
        }
    }

    private void InitCells()
    {
        cells = new List<InventoryCell>();

        for (int i = 0; i < cellsCount; i++)
        {
            GameObject cell = Instantiate(cellPrefab);
            cell.transform.SetParent(cellsParent.transform, false);
            cells.Add(cell.GetComponent<InventoryCell>());
        }
    }

    public void AddItem(ItemSO item, int count)
    {
        if(cells is null)
        {
            InitCells();
        }
        cells[0].PutItems(item, count);
    }
}
