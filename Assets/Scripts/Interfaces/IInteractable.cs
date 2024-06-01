using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Enums.InteractionType InteractionType { get; }
    void OnInteraction();
}
