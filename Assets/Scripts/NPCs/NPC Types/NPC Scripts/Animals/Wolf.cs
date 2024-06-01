using System.Collections.Generic;
using UnityEngine;

public class Wolf : NPCBase
{
    [Space]
    [SerializeField] private List<ItemSO> befriendingItemsList;
    [SerializeField] private List<ItemSO> feedingList;

    private bool isBefriended = false;
    public bool IsBefriended => isBefriended;
    override public void OnInteraction(Player player)
    {
        // ItemSO selectedItem = GameManager.Instance.InventoryController.GetSelectedItem();
        ItemSO selectedItem = player.SelectedItem;

        if(selectedItem != null)
        {
            if(befriendingItemsList.Count > 0)
            {
                if(befriendingItemsList.Contains(selectedItem))
                {
                    befriendingItemsList.Remove(selectedItem);
                    
                    if(befriendingItemsList.Count == 0)
                    {
                        Befriend(player);
                        GameManager.Instance.InventoryController.ConsumeSelectedItem();
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
