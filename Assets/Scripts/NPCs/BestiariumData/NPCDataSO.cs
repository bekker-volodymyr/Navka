using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDescriptionSO", menuName = "NPCLogic/Description/NPCDescriptionSO")]
public class NPCDataSO : ScriptableObject
{
    public int HealthPoints { get; set; }
    public Enums.NPCType Type { get; set; }
    public Enums.NPCApproach approach { get; set; }
    public Item[] Loot { get; set; }
    public string Name { get; set; }
    public string Weaknesses { get; set; }
    public string Behaviour { get; set; }
    public string Lore { get; set; }
    public Sprite Picture { get; set; }
}
