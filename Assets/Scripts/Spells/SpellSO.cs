using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellSO", menuName = "Spells/SpellSO")]
public class SpellSO : ScriptableObject
{
    [SerializeField] public string Title;
    [SerializeField] public string Description;
    [SerializeField] public int ManaCost;
    [SerializeField] public float Cooldown;
    [SerializeField] public Sprite Picture;
}
