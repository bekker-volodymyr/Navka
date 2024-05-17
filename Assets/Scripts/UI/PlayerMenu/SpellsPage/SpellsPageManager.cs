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
    public void SwitchSpell(SpellSO spell)
    {
        Spell_Picture.sprite = spell.Picture;
        Spell_Title.SetText(spell.Title);
        Spell_Description.SetText(spell.Description);
        Spell_ManaCost.SetText(spell.ManaCost.ToString());
        Spell_Cooldown.SetText(spell.Cooldown.ToString());
    }
}
