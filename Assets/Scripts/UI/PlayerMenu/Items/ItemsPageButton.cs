using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsPageButton : MonoBehaviour
{
    public ItemsPageManager pageManager;

    public ItemSO item;

    public void InitButton(ItemSO item)
    {
        this.item = item;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(item.name);
    }

    public void OnClick()
    {
        pageManager.SwitchItem(item);
    }
}
