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

    [Space]
    [SerializeField] private NPCBefriendedSOBase defaultBefriendedStateBase;
    private NPCBefriendedSOBase defaultBefriendedStateInstance;
    [SerializeField] private NPCBefriendedSOBase secondaryBefriendedStateBase;
    private NPCBefriendedSOBase secondaryBefriendedStateInstance;
    public NPCBefriendedSOBase CurrentBefriendedStateInstance { get; set; }
    public NPCBefriendedState BefriendedState { get; set; }

    override protected void Awake()
    {
        base.Awake();
        defaultBefriendedStateInstance = Instantiate(defaultBefriendedStateBase);
        secondaryBefriendedStateInstance = Instantiate(secondaryBefriendedStateBase);

        CurrentBefriendedStateInstance = defaultBefriendedStateInstance;

        BefriendedState = new NPCBefriendedState(this, StateMachine);
    }

    override protected void Start()
    {
        base.Start();
        Debug.Log($"{gameObject.name} befriendable start");

        CurrentBefriendedStateInstance.Initialize(this);
    }

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
        player.Befriend(this);
        StateMachine.ChangeState(BefriendedState);
    }

    public void SetDafaultState()
    {
        CurrentBefriendedStateInstance = defaultBefriendedStateInstance;
    }

    public void SetSecondaryState()
    {
        CurrentBefriendedStateInstance = secondaryBefriendedStateInstance;
    }
}
