using System.Collections.Generic;
using UnityEngine;

public class Wolf : BefriendableNPC
{
    [Space]
    [SerializeField] private ItemSO dropItem;

    //override public void OnInteraction(GameObject interactObject)
    //{
    //    Player player = interactObject.GetComponent<Player>();

    //    if (player == null) 
    //    {
    //        return;        
    //    }

    //    ItemSO selectedItem = player.SelectedItem;

    //    if(selectedItem != null)
    //    {
    //        if(befriendingItemsList.Count > 0)
    //        {
    //            if(befriendingItemsList.Contains(selectedItem))
    //            {
    //                befriendingItemsList.Remove(selectedItem);
    //                player.FeedItem();

    //                if (befriendingItemsList.Count == 0)
    //                {
    //                    Befriend(player);
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        SpawnItem(dropItem, 1);
    //    }
    //}

    //private void Befriend(Player player)
    //{
    //    Debug.Log($"{gameObject.name} befriended now");

    //    isBefriended = true;
    //    befriendedPlayer = player;
    //    StateMachine.ChangeState(BefriendedState);
    //}
}
