using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemsPageManager: MonoBehaviour
{
    [SerializeField] private ItemStorageSO itemsStorage;
    [SerializeField] private GameObject buttonsFood;
    [SerializeField] private GameObject buttonsAmulets;
    [SerializeField] private GameObject buttonsPotions;
    [SerializeField] private GameObject buttonsComponents;

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private Image Item_Image;
    [SerializeField] private TextMeshProUGUI Item_Name;
    [SerializeField] private TextMeshProUGUI Item_Description;
    [SerializeField] private TMP_SubMeshUI Item_Recepies;
    [SerializeField] private TMP_SubMeshUI Item_Sources;
    [SerializeField] private TMP_SubMeshUI Item_Type;
    [SerializeField] private TMP_SubMeshUI Item_Effect;


    public void Initialize()
    {

        foreach (var item in itemsStorage.items)
        {
            GameObject newButton; //get item page button

            switch (item.Type)
            {
                case Enums.ItemType.Food:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsFood.transform, false);
                    break;
                case Enums.ItemType.Amulet:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsAmulets.transform, false);
                    break;
                case Enums.ItemType.Potion:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsPotions.transform, false);
                    break;
                case Enums.ItemType.Component:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsComponents.transform, false);
                    break;
                default:
                    throw new Exception("uknown type exception");
                    
            }
        }
    }


    public void SwitchButtonList(Enums.ItemType type)
    {
        if(type == Enums.ItemType.Amulet)
        {
            buttonsAmulets.SetActive(true);
        }
        else if (type == Enums.ItemType.Component)
        {
            buttonsComponents.SetActive(true);
        }
        else if (type == Enums.ItemType.Food)
        {
            buttonsFood.SetActive(true);
        }
        else if (type == Enums.ItemType.Potion)
        {
            buttonsPotions.SetActive(true);
        }
    } 

    public void SwitchItem(ItemSO item)
    {
        Item_Image.sprite= item.Sprite;
        Item_Name.SetText(item.Name);
        Item_Description.SetText(item.Lore); //?
        //Item_Recepies.textComponent.SetText(item.); -- missing
        Item_Sources.textComponent.SetText(item.Sources.ToString());
        Item_Type.textComponent.SetText(item.Type.ToString());
        Item_Effect.textComponent.SetText(item.Effect);
    }
}
