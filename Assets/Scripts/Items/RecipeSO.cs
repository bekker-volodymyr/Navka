using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RecipeSO", menuName ="Items/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    [Space]
    [SerializeField] private List<ItemSO> recipe;
    public List<ItemSO> Recipe => recipe;

    [SerializeField] private ItemSO result;
    public ItemSO Result => result;
}
