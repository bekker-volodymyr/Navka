using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
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
