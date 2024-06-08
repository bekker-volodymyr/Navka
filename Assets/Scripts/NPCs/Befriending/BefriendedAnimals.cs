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

    public void SetTarget(GameObject target)
    {
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i].defendsPlayer)
            {
                animals[i].SetTarget(target);
            }
        }
    }
}