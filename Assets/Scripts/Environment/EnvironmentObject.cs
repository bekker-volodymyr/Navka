using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour, IInteractable
{
    private Enums.InteractionType interactType = Enums.InteractionType.EnvironmentObject;
    public Enums.InteractionType InteractionType => interactType;

    public void OnInteraction(Player player)
    {
        Debug.Log("No interaction or not implemented");
    }
}
