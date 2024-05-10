using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string Title;
    public string Lore;
    public List<RecipeSO> Recipes;
    public string Sources;
    public Enums.ItemType Type;
    public List<string> Effects;
    public Sprite Sprite;
    public int maxPerStack;
}
