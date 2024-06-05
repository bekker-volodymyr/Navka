using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : NPCBase
{
    [Space]
    [SerializeField] private ItemSO dropItem;

    public override void OnInteraction(GameObject interactObject)
    {
        if (interactObject.GetComponent<Player>() == null) return;

        SpawnItem(dropItem, 1);
    }
}
