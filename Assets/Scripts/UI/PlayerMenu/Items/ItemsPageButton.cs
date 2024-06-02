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
        text.SetText(item.Name);
    }

    public void OnClick()
    {
        pageManager.SwitchItem(item);
    }
}
