using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SpellsPageButton : MonoBehaviour
{
    private SpellSO Spell;

    private SpellsPageManager spellsPageManager;
    public void InitButton(SpellSO item, SpellsPageManager manager)
    {
        this.Spell = item;
        spellsPageManager = manager;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(item.SpellDescription.Name);
    }
    public void OnClick()
    {
        spellsPageManager.SwitchSpell(Spell);
    } 
}
