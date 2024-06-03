using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog-Default Dialog", menuName = "NPC Logic/Dialog Logic/Default Dialog")]
public class NPCDefaultDialog : NPCDialogSOBase
{
    public override void Initialize(NPCBase npc)
    {
        base.Initialize(npc);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }
    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
    public override void DoAnimationTriggerEventLogic(NPCBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
    
    public override void ResetValues()
    {
        base.ResetValues();
    }
}
