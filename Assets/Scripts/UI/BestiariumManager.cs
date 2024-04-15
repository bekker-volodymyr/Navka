using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BestiariumManager: MonoBehaviour 
{
    [SerializeField] private NPCDescriptionStorageSO NPCStorage;
    [SerializeField] private GameObject buttonsHumanNPC;
    [SerializeField] private GameObject buttonsUnholyEntityNPC;
    [SerializeField] private GameObject buttonsAnimalNPC;
    [SerializeField] private GameObject buttonsGodNPC;

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private Image NPC_Picture;
    [SerializeField] private TextMeshProUGUI NPC_Name;
    [SerializeField] private TextMeshProUGUI NPC_Lore;
    [SerializeField] private TextMeshProUGUI NPC_HealthPoints;
    [SerializeField] private TextMeshProUGUI NPC_Type;
    [SerializeField] private TextMeshProUGUI NPC_Approach;
    [SerializeField] private TextMeshProUGUI NPC_Loot;
    [SerializeField] private TextMeshProUGUI NPC_Weaknesses;
    [SerializeField] private TextMeshProUGUI NPC_Behaviour;
    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (var item in NPCStorage.NPCDescriptions)
        {
            GameObject newButton; 

            switch (item.Type)
            {
                case Enums.NPCType.Human:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsHumanNPC.transform, false);
                    newButton.GetComponent<NPCBestiariumButton>().InitButton(item, this);
                    break;
                case Enums.NPCType.UnholyEntitiy:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsUnholyEntityNPC.transform, false);
                    newButton.GetComponent<NPCBestiariumButton>().InitButton(item, this);
                    break;
                case Enums.NPCType.Animal:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsAnimalNPC.transform, false);
                    newButton.GetComponent<NPCBestiariumButton>().InitButton(item, this);
                    break;
                case Enums.NPCType.God:
                    newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(buttonsGodNPC.transform, false);
                    newButton.GetComponent<NPCBestiariumButton>().InitButton(item, this);
                    break;
                default:
                    throw new Exception("uknown type exception");
                    
            }
        }
    }
    public void SwitchButtonList(int type)
    {
        buttonsHumanNPC.SetActive(false);
        buttonsUnholyEntityNPC.SetActive(false);
        buttonsAnimalNPC.SetActive(false);
        buttonsGodNPC.SetActive(false);

        switch (type)
        {
            case 1:
                buttonsHumanNPC.SetActive(true); break;
            case 2:
                buttonsUnholyEntityNPC.SetActive(true); break;
            case 3:
                buttonsAnimalNPC.SetActive(true); break;
            case 4:
                buttonsGodNPC.SetActive(true); break;
            default:
                throw new Exception("uknown type exception");
        }
    }
    public void SwitchNPC(NPCDescriptionSO Name)
    {
        NPC_Picture.sprite = Name.Picture;
        NPC_Name.SetText(Name.Name);
        NPC_Lore.SetText(Name.Lore);
        NPC_HealthPoints.SetText(Name.HealthPoints.ToString());
        NPC_Type.SetText(Name.Type.ToString());
        NPC_Approach.SetText(Name.Approach.ToString());
        NPC_Loot.SetText(Name.Loot.ToString());
        NPC_Weaknesses.SetText(Name.Weaknesses);
        NPC_Behaviour.SetText(Name.Behaviour);
    }
}
