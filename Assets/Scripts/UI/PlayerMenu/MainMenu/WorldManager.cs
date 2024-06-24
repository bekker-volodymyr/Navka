using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class WorldManager : MonoBehaviour
{
    public void CreateNewWorld()
    {
        SceneManager.LoadScene("Campaign");
    }

    public void BackToMainMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene(0);
    }

    public void JoinOnline()
    {
        SceneManager.LoadScene("ConnectToServer");
    }
}
