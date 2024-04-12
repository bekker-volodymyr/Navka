using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string Name;
    public string Lore;
    public List<RecipeSO> Recipes;
    public List<string> Sources;
    public Enums.ItemType Type;
    public string Effect;
    public Sprite Sprite;
}
