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

    public void PutItems(ItemSO newItem, int countOfNewItem)
    {
        item = newItem;
        count = countOfNewItem;
        countText.text = count.ToString();
        image.sprite = item.Sprite;
        image.gameObject.SetActive(true);
    }

}
