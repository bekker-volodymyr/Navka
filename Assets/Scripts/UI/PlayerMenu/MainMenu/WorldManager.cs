using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public void CreateNewWorld()
    {
        SceneManager.LoadScene("Campaign");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void JoinOnline()
    {
        SceneManager.LoadScene("ConnectToServer");
    }
}
