using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelesTotem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject velesTotemMenu;
    public void OnInteraction(GameObject interactObject)
    {
        //velesTotemMenu.SetActive(true);
    }
}
