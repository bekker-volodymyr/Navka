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
        cells = new List<InventoryCell>();

        for(int i = 0; i < cellsCount - 1; i++)
        {
            GameObject cell = Instantiate(cellPrefab);
            cell.transform.SetParent(cellsParent.transform, false);
            cells.Add(cell.GetComponent<InventoryCell>());
        }
    }

    public void AddItem(ItemSO item, int count)
    {
        cells[0].PutItems(item, count);
    }
}
