using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellsPageManager : MonoBehaviour
{
    [SerializeField] private SpellsStorageSO spellsStorage;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Image Spell_Picture;
    [SerializeField] private TextMeshProUGUI Spell_Title;
    [SerializeField] private TextMeshProUGUI Spell_Description;
    [SerializeField] private TextMeshProUGUI Spell_ManaCost;
    [SerializeField] private TextMeshProUGUI Spell_Cooldown;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (var item in spellsStorage.Spells)
        {
            GameObject newButton; 
            newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(buttonsParent.transform, false);
            newButton.GetComponent<SpellsPageButton>().InitButton(item, this); 
        }
    }
    public void SwitchSpell(SpellSO Name)
    {
        Spell_Picture.sprite = Name.Picture;
        Spell_Title.SetText(Name.Title);
        Spell_Description.SetText(Name.Description);
        Spell_ManaCost.SetText(Name.ManaCost.ToString());
        Spell_Cooldown.SetText(Name.Cooldown.ToString());
    }
}
