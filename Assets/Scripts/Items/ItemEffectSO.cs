using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEffectSO", menuName = "Items/ItemEffectSO")]
public class ItemEffectSO : ScriptableObject
{
    [Space]
    [SerializeField] private Enums.EffectProperty effectProperty;
    public Enums.EffectProperty EffectProperty => effectProperty;
    [SerializeField] private float value;
    public float Value => value;
}
