using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDescriptionSO", menuName = "NPC Description/NPCDescriptionSO")]
public class NPCDescriptionSO : ScriptableObject
{
    [Space]
    [SerializeField] private int healthPoints;
    public int HealthPoints => healthPoints;
    [SerializeField] private float basicDamage;
    public float BasicDamage => basicDamage;
    
    [Space]
    [SerializeField] private Enums.NPCType type;
    public Enums.NPCType Type => type;
    [SerializeField] private Enums.NPCApproach approach;
    public Enums.NPCApproach Approach => approach;
    [SerializeField] private List<NPCDescriptionSO> attackTargets;
    public List<NPCDescriptionSO> AttackTargets => attackTargets;

    [Space]
    [SerializeField] private List<ItemSO> loot;
    public List<ItemSO> Loot => loot;
    [SerializeField] private List<float> lootChance;
    public List<float> LootChance => lootChance;


    [Space]
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] public string description;
    public string Description => description;
    [SerializeField] public string befriending;
    public string Befriending => befriending;
    [SerializeField] public string lore;
    public string Lore => lore;
    [SerializeField] private Sprite picture;
    public Sprite Picture => picture;

}
