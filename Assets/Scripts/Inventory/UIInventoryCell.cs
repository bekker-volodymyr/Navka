using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image image;
    [SerializeField] private Image selectedBorder;

    public bool empty = true;

    private bool selected = false;

    public event Action<UIInventoryCell> ItemSelected;
    public event Action<UIInventoryCell> ItemDeselectByClick;

    private void Awake()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        quantityText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        empty = true;
        Deselect();
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

    private void Select()
    {
        if (!empty)
        {
            ItemSelected?.Invoke(this);
            selectedBorder.gameObject.SetActive(true);
            selected = true;
        }
    }

    public bool TrySelect()
    {
        if (!empty) Select();

        return !empty;
    }

    private void DeselectByClick()
    {
        ItemDeselectByClick?.Invoke(this);
        Deselect();
    }

    public void Deselect()
    {
        selectedBorder.gameObject.SetActive(false);
        selected = false;
    }

    public void PointerClick()
    {
        if (selected)
        {
            DeselectByClick();
        }
        else
        {
            Select();
        }
    }
}
