using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Line", menuName ="Dialog System/Character Line")]
public class CharacterLine : ScriptableObject
{
    [SerializeField]
    public string line;
    [SerializeField]
    public List<PlayerAnswers> answers;
}
