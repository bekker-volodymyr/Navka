using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBestiariumButton: MonoBehaviour
{
    [SerializeField] private NPCDescriptionSO NPCDescription;

    private BestiariumManager bestiariumManager;

    private void Start()
    {
        bestiariumManager = FindObjectOfType<BestiariumManager>();
    }

    void OnClick()
    {
        bestiariumManager.SwitchNPC(NPCDescription);
    } 
}
