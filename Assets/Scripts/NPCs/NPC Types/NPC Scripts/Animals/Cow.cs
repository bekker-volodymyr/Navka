using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : NPCBase
{
    [Space]
    [SerializeField] private ItemSO dropItem;

    public override void OnInteraction(Player player)
    {
        DropItem(dropItem, 1);
    }
}
