using System.Collections.Generic;
using UnityEngine;

public class Wolf : NPCBase
{
    [Space]
    [SerializeField] private List<ItemSO> befriendingItemsList;
    [SerializeField] private List<ItemSO> feedingList;

    [Space]
    [SerializeField] private ItemSO dropItem;

    private bool isBefriended = false;
    public bool IsBefriended => isBefriended;
    override public void OnInteraction(Player player)
    {
        ItemSO selectedItem = player.SelectedItem;

        if(selectedItem != null)
        {
            if(befriendingItemsList.Count > 0)
            {
                if(befriendingItemsList.Contains(selectedItem))
                {
                    befriendingItemsList.Remove(selectedItem);
                    player.FeedItem();

                    if (befriendingItemsList.Count == 0)
                    {
                        Befriend(player);
                    }
                }
            }
        }
        else
        {
            SpawnItem(dropItem, 1);
        }
    }

    private void Befriend(Player player)
    {
        Debug.Log($"{gameObject.name} befriended now");

        isBefriended = true;
        befriendedPlayer = player;
        StateMachine.ChangeState(BefriendedState);
    }
}
