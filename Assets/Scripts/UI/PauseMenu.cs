using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
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
        if (Input.GetKeyUp(KeyCode.Escape))
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
        GameManager.isPaused = true;
    }
    private void HideMenu()
    {
        content.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.isPaused = false;
    }

    public void OnMenuButtonClick(int value)
    {
        // Debug.Log(value.ToString());
        switch (value)
        {
            case 1:  // Resume
                HideMenu();
                break;
            case 2:  // Settings
                //open settings menu
                break;
            case 3:  // Exit
                if (Application.isEditor)
                {
                    //EditorApplication.ExitPlaymode();
                    // EditorApplication.Exit(0);  // close editor
                }
                else
                {
                    Application.Quit(0);
                }
                break;
            default:
                Debug.LogError($"Undefined button click detected: value '{value}'");
                break;
        }
    }

}
