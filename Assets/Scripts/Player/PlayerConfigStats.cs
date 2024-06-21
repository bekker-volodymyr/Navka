using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Base Config", menuName ="Player/Base Config")]
public class PlayerConfigStats : ScriptableObject
{
    [Header("Player Stats")]
    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;
    [SerializeField]
    private float maxHunger;
    public float MaxHunger => maxHunger;
    [SerializeField]
    private float maxMana;
    public float MaxMana => maxMana;
    [SerializeField]
    private float basicDamage;
    public float BasicDamage => basicDamage;
    [SerializeField]
    private float hungerDelay;
    public float HungerDelay => hungerDelay;
    [SerializeField]
    private float manaRestoreDelay;
    public float ManaRestoreDelay => manaRestoreDelay;
}
