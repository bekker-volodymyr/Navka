using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellSO", menuName = "Spells/SpellSO")]
public class SpellSO : ScriptableObject
{
    [Space]
    [SerializeField] private SpellDescriptionSO spellDescription;
    public SpellDescriptionSO SpellDescription => spellDescription;

    [Space]
    [SerializeField] private SpellLogicSOBase spellLogic;
    public SpellLogicSOBase SpellLogic => spellLogic;

    [Space]
    [SerializeField] private GameObject sound;
    public GameObject Sound => sound;


}
