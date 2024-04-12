using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBestiariumButton: MonoBehaviour
{
    [SerializeField] private NPCDescriptionSO NPCDescription;

    public BestiariumManager bestiariumManager;

    void OnClick()
    {
        bestiariumManager.SwitchNPC(NPCDescription);
    } 
}
