using TMPro;
using UnityEngine;

[CreateAssetMenu (fileName ="SpellDescriptionSO", menuName ="Spells/Spell Description")]
public class SpellDescriptionSO : ScriptableObject
{
    [Space]
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private string description;
    public string Description => description;
    [SerializeField] private int manaCost;
    public int ManaCost => manaCost;
    [SerializeField] private float cooldown;
    public float Cooldown => cooldown;  
    [SerializeField] private Sprite picture;
    public Sprite Picture => picture;
}
