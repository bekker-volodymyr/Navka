using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellsPageManager : MonoBehaviour
{
    [Space]
    [SerializeField] private SpellsStorageSO spellsStorage;

    [Space]
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private GameObject buttonPrefab;

    [Space]
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI manaCost;
    [SerializeField] private TextMeshProUGUI cooldown;

    [Space]
    [SerializeField] private SpellManager spellsManager;

    private SpellSO currentSpell;

    private void Start()
    {
        Initialize();
        SwitchSpell(spellsStorage.Spells[0]);

        spellsManager = GameObject.FindGameObjectWithTag("Spells Manager").GetComponent<SpellManager>();
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
        currentSpell = spell;

        picture.sprite = spell.SpellDescription.Picture;
        title.SetText(spell.SpellDescription.Name);
        description.SetText(spell.SpellDescription.Description);
        manaCost.SetText(spell.SpellDescription.ManaCost.ToString());
        cooldown.SetText(spell.SpellDescription.Cooldown.ToString());
    }

    public void ReadyUpSpell()
    {
        spellsManager.AddSpell(currentSpell);
    }
}
