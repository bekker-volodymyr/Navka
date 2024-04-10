using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDescriptionStorageSO", menuName = "NPC Description / NPCDescriptionStorageSO")]
public class NPCDescriptionStorageSO: ScriptableObject
{
    public List<NPCDescriptionSO> NPCDescriptions;
}
