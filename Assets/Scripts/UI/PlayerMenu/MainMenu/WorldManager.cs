using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public void CreateNewWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadWorld()
    {

    }

    public void DeleteWorld()
    {

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
