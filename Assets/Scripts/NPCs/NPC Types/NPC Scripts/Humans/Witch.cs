using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : NPCBase, IDialog
{
    [Space]
    [SerializeField] private List<CharacterLine> lines;
    public List<CharacterLine> Lines { get { return lines; } }
    public NPCBase npc { get { return this; } }

    public override void OnInteraction(Player player)
    {
        StateMachine.ChangeState(DialogState);
        GameManager.DialogStartEvent?.Invoke(this);
    }
}
