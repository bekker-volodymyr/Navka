using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RecipeSO", menuName ="Items/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public List<ItemSO> recipe;
    public ItemSO result;
}
