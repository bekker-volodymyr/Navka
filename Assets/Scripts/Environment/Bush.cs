using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : ItemDropper
{
    private enum Stage
    {
        Berries, Leaves, NoLeaves
    }

    private Stage stage;

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
                item = berriesSO;
                break;
            case Stage.Leaves:
                spriteRenderer.sprite = leavesSprite;
                item = leavesSO; break;
            case Stage.NoLeaves:
                spriteRenderer.sprite = noLeavesSprite;
                item = null;
                break;
            default:
                Debug.Log($"Unknown stage type {stage}");
                break;
        }
    }

    override public void OnInteraction(Player player)
    {
        base.OnInteraction(player);

        switch (stage)
        {
            case Stage.Berries:
                spriteRenderer.sprite = leavesSprite;
                item = leavesSO;
                stage = Stage.Leaves;
                break;
            case Stage.Leaves:
                spriteRenderer.sprite = noLeavesSprite;
                item = null;
                stage = Stage.NoLeaves;
                break;
            default:
                // TODO: animation
                break;
        }
    }
}
