using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isOpened = false;

    public void ToggleMenu()
    {
        if (!isOpened)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }

        isOpened = !isOpened;
    }
}
