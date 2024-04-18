using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image image;

    public void SetEmpty()
    {
        quantityText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    public void SetItem(ItemSO item, int quantity)
    {
        // Set quantity
        quantityText.gameObject.SetActive(true);
        quantityText.text = quantity.ToString();

        // Set image
        image.gameObject.SetActive(true);
        image.sprite = item.Sprite;
    }
}
