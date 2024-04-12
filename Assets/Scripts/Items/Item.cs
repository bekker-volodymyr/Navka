using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemDescription;
    [SerializeField] int count;

    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField]
    private TMPro.TextMeshProUGUI pickUpText;

    private bool pickUpAllowed;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);

        // inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update()
    {
        //if (pickUpAllowed && Input.GetKeyDown(KeyCode.R))
        //    PickUp();
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
        inventoryManager.AddItem(itemDescription, count);
        Destroy(gameObject);
    }

    public void OnInteraction()
    {
        PickUp();
    }
}
