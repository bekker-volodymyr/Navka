using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEffectSO", menuName = "Items/ItemEffectSO")]
public class ItemEffectSO : ScriptableObject
{
    public Enums.EffectProperty EffectProperty;

    public float Value;
}
