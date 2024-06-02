using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    private ItemSO item = null;
    private int count = 0;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Image image;

    public int TryPutItems(ItemSO newItem, int quantity)
    {
        if(item is null)
        {
            int newQuantity = quantity <= newItem.MaxPerStack ? quantity : newItem.MaxPerStack;
            PutItem(newItem, newQuantity);

            Debug.Log($"quantity: {quantity} ---- newQuantitiy: {newQuantity}");

            return quantity - newQuantity;
        }

        if(item.Name == newItem.Name)
        {
            int newQuantity = count + quantity;
            if(newQuantity <= newItem.MaxPerStack)
            {
                count = newQuantity;
                countText.text = count.ToString();
                return 0;
            }
            else
            {
                int left = newQuantity - newItem.MaxPerStack;
                count = newItem.MaxPerStack;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
        else
        {
            // TODO: 
            // TAKE ITEM AND DRAG IT
        }
    }

    private void DropItem()
    {
        
    }
}
