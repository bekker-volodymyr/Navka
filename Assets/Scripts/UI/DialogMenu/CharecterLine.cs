using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Line", menuName ="Dialog System/Character Line")]
public class CharacterLine : ScriptableObject
{
    [SerializeField] private string line;
    public string Line => line;
    [SerializeField] private List<PlayerAnswers> answers;
    public List<PlayerAnswers> Answers => answers;
    [SerializeField] private QuestsSO quest;
    public QuestsSO Quest => quest;
}
