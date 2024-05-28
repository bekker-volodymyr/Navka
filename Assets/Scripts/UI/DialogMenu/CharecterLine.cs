using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Cherecter Line", menuName ="Dialog System/Cherecter Line")]
public class CherecterLine : ScriptableObject
{
    [SerializeField]
    public string line;
    [SerializeField]
    public List<PlayerAnswers> answers;
}
