using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellsStorageSO", menuName = "Spells/SpellsStorageSO")]
public class SpellsStorageSO : ScriptableObject
{
    public List<SpellSO> Spells;
}
