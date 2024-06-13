using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialog
{
    CharacterLine FirstLine { get; }
    NPCBase npc { get; }
}
