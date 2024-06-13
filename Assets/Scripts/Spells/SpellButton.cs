using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    [Space]
    [SerializeField] private Button button;
    private SpellSO spell;
    public SpellSO Spell => spell;

    private SpellManager manager;

    public void InitButton(SpellSO spell, SpellManager manager)
    {
        this.spell = spell;
        button.image.sprite = spell.SpellDescription.Picture;
        this.manager = manager;
    }

    public void ActivateSpell()
    {
        manager.ActivateSpell(spell);
        button.interactable = false;
        StartCoroutine(CooldownCoroutine());

    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(spell.SpellDescription.Cooldown);
        button.interactable = true;
    }
    
}
