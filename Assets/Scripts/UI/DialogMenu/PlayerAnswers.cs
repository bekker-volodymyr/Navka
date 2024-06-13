using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnswers
{
    [SerializeField] private string playerResponse;
    public string PlayerResponse => playerResponse;
    [SerializeField] private CharacterLine nextLine;
    public CharacterLine NextLine => nextLine;
    [SerializeField] private bool isFinalLine;
    public bool IsFinalLine => isFinalLine;
    [SerializeField] private QuestsSO quest;
    public QuestsSO Quest => quest;
}
