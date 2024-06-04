using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : ItemDropper, IInteractable
{
    private enum Stage
    {
        Berries, Leaves, NoLeaves
    }

    private Stage stage;

    private Enums.InteractionType interactType = Enums.InteractionType.EnvironmentObject;
    public Enums.InteractionType InteractionType => interactType;

    [Space]
    [SerializeField] private Sprite berriesSprite;
    [SerializeField] private Sprite leavesSprite;
    [SerializeField] private Sprite noLeavesSprite;

    [Space]
    [SerializeField] private ItemSO berriesSO;
    [SerializeField] private ItemSO leavesSO;

    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        stage = (Stage)Random.Range(0, 3);
        switch (stage)
        {
            case Stage.Berries:
                spriteRenderer.sprite = berriesSprite;
                break;
            case Stage.Leaves:
                spriteRenderer.sprite = leavesSprite;
                break;
            case Stage.NoLeaves:
                spriteRenderer.sprite = noLeavesSprite;
                break;
            default:
                Debug.Log($"Unknown stage type {stage}");
                break;
        }
    }

    public void OnInteraction(Player player)
    {
        ItemSO itemToDrop = null;

        switch (stage)
        {
            case Stage.Berries:
                spriteRenderer.sprite = leavesSprite;
                itemToDrop = berriesSO;
                stage = Stage.Leaves;
                break;
            case Stage.Leaves:
                spriteRenderer.sprite = noLeavesSprite;
                itemToDrop = leavesSO;
                stage = Stage.NoLeaves;
                break;
            default:
                // TODO: animation
                break;
        }

        SpawnItem(itemToDrop, 1);
    }
}
