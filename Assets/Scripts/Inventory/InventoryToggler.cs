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
    private bool toggler;

    public void ToggleInventoryMenu()
    {
        if (!isOpened)
        {
            animator.SetTrigger("Open");
            inventoryPnl.SetActive(true);
            questsPnl.SetActive(false);
            toggler = true;
            isOpened = !isOpened;
            
        }
        else
        {
            if (!toggler)
            {
                inventoryPnl.SetActive(true);
                questsPnl.SetActive(false);
                toggler = true;
            }
            else
            {
                animator.SetTrigger("Close");
                isOpened = !isOpened;
            }
        }       
    }

    public void ToggleQuestsMenu()
    {
        if (!isOpened)
        {
            animator.SetTrigger("Open");
            inventoryPnl.SetActive(false);
            questsPnl.SetActive(true);
            toggler = false;
            isOpened = !isOpened;
        }
        else
        {
            if (toggler)
            {
                inventoryPnl.SetActive(false);
                questsPnl.SetActive(true);
                toggler = false;
            }
            else
            {
                animator.SetTrigger("Close");
                isOpened = !isOpened;
            }
        }
    }
}
