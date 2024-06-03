using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelesTotem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject velesTotemMenu;
    private Enums.InteractionType interactType;
    public Enums.InteractionType InteractionType { get { return interactType; } }
    public void OnInteraction(Player player)
    {
        velesTotemMenu.SetActive(true);
    }
}
