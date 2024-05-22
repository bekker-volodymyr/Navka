using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDescriptionSO", menuName = "NPC Description/NPCDescriptionSO")]
public class NPCDescriptionSO : ScriptableObject
{
    [SerializeField] public int HealthPoints;
    [SerializeField] public float BasicDamage;
    [SerializeField] public Enums.NPCType Type;
    [SerializeField] public Enums.NPCApproach Approach;
    [SerializeField] public ItemSO[] Loot;
    [SerializeField] public string Name;
    [SerializeField] public string Weaknesses;
    [SerializeField] public string Behaviour;
    [SerializeField] public string Befriending;
    [SerializeField] public string Lore;
    [SerializeField] public Sprite Picture;
    [SerializeField] public List<NPCDescriptionSO> AttackTargets;
}
