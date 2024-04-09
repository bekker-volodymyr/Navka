using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBestiariumButton
{
    NPCDescriptionSO NPCDescription;
    void OnClick()
    {
        BestiariumManager.SwitchNPC(NPCDescription);
    } 
}
