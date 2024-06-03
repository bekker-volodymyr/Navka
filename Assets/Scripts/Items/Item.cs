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

    private Enums.InteractionType interactType = Enums.InteractionType.TakeItem;
    public Enums.InteractionType InteractionType { get { return interactType; } }

    [SerializeField] private TextMeshProUGUI interactionText;
    
    public static event Action<ItemSO, int> OnPickUp;

    private bool pickUpAllowed;

    private void Start()
    {
        interactionText.gameObject.SetActive(false);
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = item.Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactionText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
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
