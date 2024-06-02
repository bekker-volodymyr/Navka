using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BestiariumManager: MonoBehaviour 
{
    [Space]
    [SerializeField] private NPCDescriptionStorageSO Storage;

    [Space]
    [SerializeField] private GameObject buttonsHuman;
    [SerializeField] private GameObject buttonsUnholyEntity;
    [SerializeField] private GameObject buttonsAnimal;
    [SerializeField] private GameObject buttonsGod;

    [Space]
    [SerializeField] private GameObject buttonPrefab;

    [Space]
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI loreOrBefriending;

    [Space]
    [SerializeField] private GameObject lootParentPrefab;
    [SerializeField] private GameObject descriptionParent;
    private GameObject lootParentGO;

    [Space]
    [SerializeField] private Sprite selectedButtonSprite;
    [SerializeField] private Sprite unselectedButtonSprite;

    [Space]
    [SerializeField] private List<Button> npcTypeButtons;

    private NPCDescriptionSO defaultHuman = null;
    private NPCDescriptionSO defaultUnholy = null;
    private NPCDescriptionSO defaultAnimal = null;
    private NPCDescriptionSO defaultGod = null;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (var item in Storage.NPCDescriptions)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.GetComponent<NPCBestiariumButton>().InitButton(item, this);

            switch (item.Type)
            {
                case Enums.NPCType.Human:
                    newButton.transform.SetParent(buttonsHuman.transform, false);
                    if (defaultHuman is null) defaultHuman = item;
                    break;
                case Enums.NPCType.UnholyEntitiy:
                    newButton.transform.SetParent(buttonsUnholyEntity.transform, false);
                    if(defaultUnholy is null) defaultUnholy = item;
                    break;
                case Enums.NPCType.Animal:
                    newButton.transform.SetParent(buttonsAnimal.transform, false);
                    if(defaultAnimal is null) defaultAnimal = item;
                    break;
                case Enums.NPCType.God:
                    newButton.transform.SetParent(buttonsGod.transform, false);
                    if (defaultGod is null) defaultGod = item;
                    break;
                default:
                    throw new Exception("uknown type exception");
                    
            }
        }

        SwitchButtonList(1);
        SwitchNPC(defaultHuman);
    }
    public void SwitchButtonList(int type)
    {
        buttonsHuman.SetActive(false);
        buttonsUnholyEntity.SetActive(false);
        buttonsAnimal.SetActive(false);
        buttonsGod.SetActive(false);

        switch (type)
        {
            case 1:
                buttonsHuman.SetActive(true);
                SwitchNPC(defaultHuman);
                break;
            case 2:
                buttonsUnholyEntity.SetActive(true);
                SwitchNPC(defaultUnholy);
                break;
            case 3:
                buttonsAnimal.SetActive(true);
                SwitchNPC(defaultAnimal);
                break;
            case 4:
                buttonsGod.SetActive(true);
                SwitchNPC(defaultGod);
                break;
            default:
                throw new Exception("uknown type exception");
        }
    }
    public void SwitchNPC(NPCDescriptionSO npc)
    {
        _name.SetText(npc.Name);

        picture.sprite = npc.Picture;
        picture.SetNativeSize();

        description.SetText(npc.description);

        if (npc.Type == Enums.NPCType.Animal)
        {
            loreOrBefriending.SetText(npc.Befriending);
        }
        else
        {
            loreOrBefriending.SetText(npc.Lore);
        }

        Destroy(lootParentGO);

        lootParentGO = Instantiate(lootParentPrefab, descriptionParent.transform, false);

        foreach(var item in npc.Loot)
        {
            GameObject lootItemGO = new GameObject();
            Image lootIcon = lootItemGO.AddComponent<Image>();
            lootIcon.sprite = item.Sprite;
            lootIcon.SetNativeSize();
            Instantiate(lootIcon);
            lootIcon.transform.SetParent(lootParentGO.transform, false);
        }
    }
}
