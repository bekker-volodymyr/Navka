using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    private ItemSO item = null;
    private int count = 0;
    private int maxCount = 0;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image image;

    public int TryPutItems(ItemSO newItem, int countOfNewItem)
    {
        if(item is null)
        {
            PutItem(newItem, countOfNewItem);
            return 0;
        }

        if(item._Name == newItem._Name)
        {
            count += countOfNewItem;
            countText.text = count.ToString();
            return 0;
        }

        return countOfNewItem;
    }

    public void PutItem(ItemSO newItem, int countOfNewItem)
    {
        item = newItem;
        count = countOfNewItem;
        countText.text = count.ToString();
        countText.gameObject.SetActive(true);
        image.sprite = item.Sprite;
        image.gameObject.SetActive(true);
    }

}
