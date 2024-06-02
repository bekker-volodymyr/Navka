using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Space]
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private string description;
    public string Description => description;
    [SerializeField] private string lore;
    public string Lore => lore;

    [Space]
    [SerializeField] private RecipeSO recipe;
    public RecipeSO Recipe => recipe;

    [Space]
    [SerializeField] private Enums.ItemType type;
    public Enums.ItemType Type => type;
    [SerializeField] private ItemEffectSO effect;
    public ItemEffectSO Effect => effect;

    [Space]
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] private int maxPerStack;
    public int MaxPerStack => maxPerStack;

}
