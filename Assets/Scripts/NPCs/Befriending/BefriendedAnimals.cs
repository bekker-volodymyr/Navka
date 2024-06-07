using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendedAnimals : MonoBehaviour
{
    private List<BefriendableNPC> animals = new List<BefriendableNPC>();
    public List<BefriendableNPC> Animals { get { return animals; } }

    public event Action<BefriendableNPC> AddAnimalEvent;

    public void AddAnimal(BefriendableNPC animal)
    {
        animals.Add(animal);
        AddAnimalEvent?.Invoke(animal);
    }
}