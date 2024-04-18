using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO item;
    [SerializeField] int quantity;

    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private TextMeshProUGUI pickUpText;

    private bool pickUpAllowed;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);

        inventoryManager = FindObjectOfType<InventoryManager>(true);
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
        inventoryManager.AddItem(item, quantity);
        Destroy(gameObject);
    }

    public void OnInteraction()
    {
        PickUp();
    }
}
