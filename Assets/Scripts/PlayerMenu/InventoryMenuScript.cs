using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;



    void Start()
    {
        if (content.activeInHierarchy)
        {
            ShowMenu();
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (content.activeInHierarchy)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }
    private void ShowMenu()
    {
        content.SetActive(true);
        Time.timeScale = 0f;
        GameStateScript.isPaused = true;
    }
    private void HideMenu()
    {
        content.SetActive(false);
        Time.timeScale = 1.0f;
        GameStateScript.isPaused = false;
    }

}
