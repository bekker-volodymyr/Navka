using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum NPCType
    {
        Human = 1,
        UnholyEntitiy = 2,
        Animal = 3,
        God = 4
    }

    public enum NPCApproach
    {
        Agressive = 1,
        Neutral = 2
    }

    public enum ItemType
    {
        Component, Food, Amulet, Potion
    }

    public enum EffectProperty
    {
        Health, Hunger, Mana, Speed, Nothing
    }

    public enum InteractionType
    {
        Dialog, Feed, TakeItem, EnvironmentObject, Special, None
    }

    public enum TargetDecisions
    {
        Ignore, Chase, Runaway
    }
}
