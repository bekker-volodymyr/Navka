using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO item;
    [SerializeField] private int quantity;
    
    public static event Action<ItemSO, int> OnPickUp;

    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = item.Sprite;
    }

    private void PickUp()
    {
        OnPickUp?.Invoke(item, quantity);
        Destroy(gameObject);
    }

    public void OnInteraction(Player player)
    {
        PickUp();
    }

    public void InitItem(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
