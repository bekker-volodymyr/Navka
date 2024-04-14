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
    [SerializeField] private TextMeshProUGUI Item_Recepies;
    [SerializeField] private TextMeshProUGUI Item_Sources;
    [SerializeField] private TextMeshProUGUI Item_Type;
    [SerializeField] private TextMeshProUGUI Item_Effect;

    private void Start()
    {
        Initialize();
    }

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
                    newButton.GetComponent<ItemsPageButton>().InitButton(item);
                    break;
                case Enums.ItemType.Amulet:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsAmulets.transform, false);
                    newButton.GetComponent<ItemsPageButton>().InitButton(item);
                    break;
                case Enums.ItemType.Potion:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsPotions.transform, false);
                    newButton.GetComponent<ItemsPageButton>().InitButton(item);
                    break;
                case Enums.ItemType.Component:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsComponents.transform, false);
                    newButton.GetComponent<ItemsPageButton>().InitButton(item);
                    break;
                default:
                    throw new Exception("uknown type exception");
                    
            }
        }
    }


    public void SwitchButtonList(int type)
    {
        if(type == 1)
        {
            buttonsAmulets.SetActive(true);
        }
        else if (type == 2)
        {
            buttonsComponents.SetActive(true);
        }
        else if (type == 3)
        {
            buttonsFood.SetActive(true);
        }
        else if (type == 4)
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
        Item_Sources.SetText(item.Sources.ToString());
        Item_Type.SetText(item.Type.ToString());
        Item_Effect.SetText(item.Effect);
    }
}
