using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestsSO", menuName = "Quests/QuestsSO")]
public class QuestsSO : ScriptableObject
{
    [SerializeField] private string questGiver;
    public string QuestGiver => questGiver;
    [SerializeField] public string description;
    public string Description => description;
}
