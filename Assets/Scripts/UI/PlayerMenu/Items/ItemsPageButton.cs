using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsPageButton : MonoBehaviour
{
    private ItemsPageManager pageManager;

    private ItemSO item;

    public void InitButton(ItemSO item, ItemsPageManager manager)
    {
        this.item = item;
        pageManager = manager;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(item.Title);
    }

    public void OnClick()
    {
        pageManager.SwitchItem(item);
    }
}
