using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject playerMenuBtn;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameState.isInPlayerMenu)
            {
                TogglePlayerMenu();
            }
            else
            {
                TogglePause();
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && !GameState.isPaused)
        {
            TogglePlayerMenu();
        }
    }

    public void TogglePause()
    {
        pauseMenu.SetActive(!GameState.isPaused);
        GameState.isPaused = !GameState.isPaused;

        pauseBtn.SetActive(!GameState.isPaused);
        playerMenuBtn.SetActive(!GameState.isPaused);

        Time.timeScale = GameState.isPaused ? 0f : 1f;
    }

    public void TogglePlayerMenu()
    {
        playerMenu.SetActive(!GameState.isInPlayerMenu);
        GameState.isInPlayerMenu = !GameState.isInPlayerMenu;

        pauseBtn.SetActive(!GameState.isInPlayerMenu);
        playerMenuBtn.SetActive(!GameState.isInPlayerMenu);

        Time.timeScale = GameState.isInPlayerMenu ? 0f : 1f;
    }

}
