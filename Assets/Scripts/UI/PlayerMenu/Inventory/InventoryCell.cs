using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    private ItemSO item = null;
    private int count = 0;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image image;

    public int TryPutItems(ItemSO newItem, int quantity)
    {
        if(item is null)
        {
            int newQuantity = quantity <= newItem.maxPerStack ? quantity : newItem.maxPerStack;
            PutItem(newItem, newQuantity);

            Debug.Log($"quantity: {quantity} ---- newQuantitiy: {newQuantity}");

            return quantity - newQuantity;
        }

        if(item.Title == newItem.Title)
        {
            int newQuantity = count + quantity;
            if(newQuantity <= newItem.maxPerStack)
            {
                count = newQuantity;
                countText.text = count.ToString();
                return 0;
            }
            else
            {
                int left = newQuantity - newItem.maxPerStack;
                count = newItem.maxPerStack;
                countText.text = count.ToString();
                return left;
            }
        }

        return quantity;
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
