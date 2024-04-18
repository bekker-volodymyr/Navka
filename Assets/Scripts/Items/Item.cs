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

    // [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private TextMeshProUGUI pickUpText;
    
    public static event Action<ItemSO, int> OnPickUp;

    private bool pickUpAllowed;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);

        //inventoryManager = FindObjectOfType<InventoryManager>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        // inventoryManager.AddItem(item, quantity);
        // Debug.Log($"PickUp Item {item.Title} {quantity}");
        // Debug.Log(OnPickUp.GetInvocationList()[0].Method.Name);
        OnPickUp?.Invoke(item, quantity);
        //GameState.OnPickUp?.Invoke(item, quantity);
        Destroy(gameObject);
    }

    public void OnInteraction()
    {
        PickUp();
    }
}
