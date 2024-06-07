using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendedAnimalsView : MonoBehaviour
{
    [Space]
    [SerializeField] private AnimalManager animalPanelPrefab;

    [Space]
    [SerializeField] private GameObject contentParent;

    public void AddAnimal(BefriendableNPC animal)
    {
        AnimalManager newManager = Instantiate(animalPanelPrefab);
        newManager.InitPanel(animal);
        newManager.transform.SetParent(contentParent.transform, false);
    }
}
