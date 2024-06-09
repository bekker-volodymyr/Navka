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

    public bool defendsPlayer = false;

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

        defaultBefriendedStateInstance.Initialize(this);
        secondaryBefriendedStateInstance.Initialize(this);
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
        pack.Clear();

        player.Befriend(this);

        attackTargets = player.DefendFromList;

        Debug.Log($"{attackTargets[0].Name}");

        SetDefaultState();
    }

    public override void ResetState()
    {
        if (!isBefriended)
        {
            base.ResetState();
            return;
        }

        StateMachine.ChangeState(BefriendedState);
    }

    public void SetDefaultState()
    {
        CurrentBefriendedStateInstance = defaultBefriendedStateInstance;
        StateMachine.ChangeState(BefriendedState);
        defendsPlayer = true;
    }

    public void SetSecondaryState()
    {
        CurrentBefriendedStateInstance = secondaryBefriendedStateInstance;
        StateMachine.ChangeState(BefriendedState);
        defendsPlayer = false;
    }

    public override void AddNPCTarget(NPCBase target)
    {
        if(!isBefriended)
        {
            base.AddNPCTarget(target);
            return;
        }

        Debug.Log($"AddNPCTarget from Wolf");
        Debug.Log(attackTargets.Contains(target.DescriptionSO));
        Debug.Log(befriendedPlayer.DefendFromList.Contains(target.DescriptionSO));
        Debug.Log(target.DescriptionSO.Name);

        if (attackTargets.Contains(target.DescriptionSO))
        {
            SetTarget(target.gameObject);
        }
    }
}