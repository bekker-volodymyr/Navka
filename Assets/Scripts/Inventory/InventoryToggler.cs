using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryToggler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject inventoryPnl;
    [SerializeField] private GameObject questsPnl;

    private bool isOpened = false;

    public void ToggleInventoryMenu()
    {
        if (!isOpened)
        {
            animator.SetTrigger("Open");
            inventoryPnl.SetActive(true);
            questsPnl.SetActive(false);
        }
        else
        {
            animator.SetTrigger("Close");
        }

        isOpened = !isOpened;
    }

    public void ToggleQuestsMenu()
    {
        if (!isOpened)
        {
            animator.SetTrigger("Open");
            inventoryPnl.SetActive(false);
            questsPnl.SetActive(true);
        }
        else
        {
            animator.SetTrigger("Close");
        }

        isOpened = !isOpened;
    }
}
