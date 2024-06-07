using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendedAnimals : MonoBehaviour
{
    private List<BefriendableNPC> animals;
    public List<BefriendableNPC> Animals { get { return animals; } }
    public void AddAnimal(BefriendableNPC animal)
    {
        animals.Add(animal);
    }
}