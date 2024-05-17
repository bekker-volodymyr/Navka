using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class NPCBestiariumButton: MonoBehaviour
{
    private NPCDescriptionSO NPCDescription;

    private BestiariumManager bestiariumManager;
    public void InitButton(NPCDescriptionSO item, BestiariumManager manager)
    {
        this.NPCDescription = item;
        bestiariumManager = manager;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(item.Name);
    }
    public void OnClick()
    {
        bestiariumManager.SwitchNPC(NPCDescription);
    } 
}
