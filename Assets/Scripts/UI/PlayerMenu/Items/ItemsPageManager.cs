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
    [SerializeField] private TextMeshProUGUI Item_Sources;

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
                    newButton.GetComponent<ItemsPageButton>().InitButton(item, this);
                    break;
                case Enums.ItemType.Amulet:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsAmulets.transform, false);
                    newButton.GetComponent<ItemsPageButton>().InitButton(item, this);
                    break;
                case Enums.ItemType.Potion:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsPotions.transform, false);
                    newButton.GetComponent<ItemsPageButton>().InitButton(item, this);
                    break;
                case Enums.ItemType.Component:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsComponents.transform, false);
                    newButton.GetComponent<ItemsPageButton>().InitButton(item, this);
                    break;
                default:
                    throw new Exception("uknown type exception");
                    
            }
        }
    }

    public void SwitchButtonList(int type)
    {
        buttonsAmulets.SetActive(false);
        buttonsComponents.SetActive(false);
        buttonsFood.SetActive(false);
        buttonsPotions.SetActive(false);

        switch (type)
        {
            case 1:
                buttonsComponents.SetActive(true); break;
            case 2:
                buttonsFood.SetActive(true); break;
            case 3:
                buttonsAmulets.SetActive(true); break;
            case 4:
                buttonsPotions.SetActive(true); break;
            default:
                throw new Exception("uknown type exception");
        }
    }

    public void SwitchItem(ItemSO item)
    {
        Item_Image.sprite = item.Sprite;
        Item_Name.SetText(item.Title);
        Item_Description.SetText(item.Lore);
        Item_Sources.SetText(item.Sources);
    }
}
