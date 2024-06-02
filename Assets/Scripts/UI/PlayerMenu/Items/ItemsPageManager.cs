using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemsPageManager: MonoBehaviour
{
    [Space]
    [SerializeField] private ItemStorageSO itemsStorage;

    [Space]
    [SerializeField] private GameObject buttonsFood;
    [SerializeField] private GameObject buttonsAmulets;
    [SerializeField] private GameObject buttonsPotions;
    [SerializeField] private GameObject buttonsComponents;

    [Space]
    [SerializeField] private GameObject buttonPrefab;

    [Space]
    [SerializeField] private GameObject descriptionParent;
    [SerializeField] private GameObject recipeParentPrefab;
    private GameObject recipeParentGO;

    [Space]
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI lore;

    private ItemSO defaultComponent;
    private ItemSO defaultFood;
    private ItemSO defaultAmulet;
    private ItemSO defaultPotion;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (var item in itemsStorage.items)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.GetComponent<ItemsPageButton>().InitButton(item, this);

            switch (item.Type)
            {
                case Enums.ItemType.Food:
                    newButton.transform.SetParent(buttonsFood.transform, false);
                    if (defaultFood is null) defaultFood = item;
                    break;
                case Enums.ItemType.Amulet:
                    newButton.transform.SetParent(buttonsAmulets.transform, false);
                    if (defaultAmulet is null) defaultAmulet = item;
                    break;
                case Enums.ItemType.Potion:
                    newButton.transform.SetParent(buttonsPotions.transform, false);
                    if (defaultPotion is null) defaultPotion = item;
                    break;
                case Enums.ItemType.Component:
                    newButton.transform.SetParent(buttonsComponents.transform, false);
                    if (defaultComponent is null) defaultComponent = item;
                    break;
                default:
                    throw new Exception($"uknown type exception {item}");
                    
            }
        }

        SwitchButtonList(1);
        SwitchItem(defaultComponent);
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
                buttonsComponents.SetActive(true);
                SwitchItem(defaultComponent);
                break;
            case 2:
                buttonsFood.SetActive(true);
                SwitchItem(defaultFood);
                break;
            case 3:
                buttonsAmulets.SetActive(true);
                SwitchItem(defaultAmulet); 
                break;
            case 4:
                buttonsPotions.SetActive(true);
                SwitchItem(defaultPotion);
                break;
            default:
                throw new Exception("uknown type exception");
        }
    }
    public void SwitchItem(ItemSO item)
    {
        picture.sprite = item.Sprite;
        title.SetText(item.Name);
        description.SetText(item.Description);
        lore.SetText(item.Lore);

        Destroy(recipeParentGO);
        recipeParentGO = Instantiate(recipeParentPrefab);
        recipeParentGO.transform.SetParent(descriptionParent.transform, false);

        if (item.Type == Enums.ItemType.Amulet || item.Type == Enums.ItemType.Potion)
        {
            foreach (var component in item.Recipe.recipe)
            {
                GameObject componentItemGO = new GameObject();
                Image componentIcon = componentItemGO.AddComponent<Image>();
                componentIcon.sprite = item.Sprite;
                componentIcon.SetNativeSize();
                Instantiate(componentIcon);
                componentIcon.transform.SetParent(recipeParentGO.transform, false);
            }
        }
    }
}
