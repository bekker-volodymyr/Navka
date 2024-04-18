using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image image;

    private bool empty = true;

    private void Awake()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        quantityText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        empty = true;
    }

    public void SetItem(Sprite sprite, int quantity)
    {
        // Set quantity
        quantityText.gameObject.SetActive(true);
        quantityText.text = quantity.ToString();

        // Set image
        image.gameObject.SetActive(true);
        image.sprite = sprite;

        // Set empty to false
        empty = false;
    }
}
