using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellsStorageSO", menuName = "Spells/Spells Storage")]
public class SpellsStorageSO : ScriptableObject
{
    [Space]
    [SerializeField] private List<SpellSO> spells;
    public List<SpellSO> Spells => spells;
}
