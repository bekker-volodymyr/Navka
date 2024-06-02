using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Space]
    [SerializeField] private string _name;
    public string Name => _name;
    [Space]
    [SerializeField] private string description;
    public string Description => description;
    [SerializeField] private string lore;
    public string Lore => lore;
    [Space]
    [SerializeField] private List<RecipeSO> recipes;
    public List<RecipeSO> Recipes => recipes;
    public string Sources;
    public Enums.ItemType Type;
    public List<string> EffectsDescriptions;
    public ItemEffectSO Effect;
    public Sprite Sprite;
    public int maxPerStack;
}
