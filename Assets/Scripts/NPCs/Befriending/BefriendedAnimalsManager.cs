using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendedAnimalsManager : MonoBehaviour
{
    [Space]
    [SerializeField] private BefriendedAnimals animals;
    
    [Space]
    [SerializeField] private BefriendedAnimalsView view;

    private void Start()
    {
        for (int i = 0; i < animals.Animals.Count; i++)
        {
            view.AddAnimal(animals.Animals[i]);
        }

        animals.AddAnimalEvent += OnNewAnimal;
    }

    private void OnNewAnimal(BefriendableNPC animal)
    {
        view.AddAnimal(animal);
    }
}
