using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [Space]
    [SerializeField] private GameObject spellParent;
    [SerializeField] private SpellButton spellButtonPrefab;

    private List<SpellSO> readySpells = new List<SpellSO>();
    private List<SpellButton> buttons = new List<SpellButton>();

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void AddSpell(SpellSO spell)
    {
        if (readySpells.Contains(spell))
        {
            readySpells.Remove(spell);
            player.RemoveSpell(spell);
            SpellButton button = buttons.Find(b => b.Spell.SpellDescription.Name == spell.SpellDescription.Name);
            buttons.Remove(button);
            Destroy(button.gameObject);
        }
        else
        {
            SpellButton newSpell = Instantiate(spellButtonPrefab);
            newSpell.InitButton(spell, this);
            newSpell.transform.SetParent(spellParent.transform, false);
            buttons.Add(newSpell);
            readySpells.Add(spell);
            player.AddSpell(spell);
        }
    }

    public void ActivateSpell(SpellSO spell)
    {
        player.ActivateSpell(spell);
    }
}
