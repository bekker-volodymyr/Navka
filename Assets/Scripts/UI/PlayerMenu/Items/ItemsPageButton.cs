using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPageButton : MonoBehaviour
{
    public ItemsPageManager pageManager;

    public ItemSO item;

    public void OnClick()
    {
        pageManager.SwitchItem(item);
    }
}
