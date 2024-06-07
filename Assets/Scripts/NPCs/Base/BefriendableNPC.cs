using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendableNPC : NPCBase
{
    [Space]
    [SerializeField] private List<ItemSO> befriendingItemsList;
    [SerializeField] private List<ItemSO> feedingList;

    private bool isBefriended = false;
    public bool IsBefriended => isBefriended;

    protected Player befriendedPlayer;
    public Player BefriendedPlayer => befriendedPlayer;

    override public void OnInteraction(GameObject interactObject)
    {
        Player player = interactObject.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        ItemSO selectedItem = player.SelectedItem;

        if (selectedItem != null)
        {
            if (isBefriended) return;

            if (befriendingItemsList.Count > 0)
            {
                if (befriendingItemsList.Contains(selectedItem))
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
    }

    private void Befriend(Player player)
    {
        Debug.Log($"{gameObject.name} befriended now");

        isBefriended = true;
        befriendedPlayer = player;
        StateMachine.ChangeState(BefriendedState);
    }
}
