using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_Text buttonText;

    // Start is called before the first frame update

    public void OnClickConnect()
    {
        //if(usernameInput.text.Length >= 1)
        //{
            //PhotonNetwork.NickName = usernameInput.text;
            //buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        //}
    }



    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
        //SceneManager.LoadScene(4);

    }
}
