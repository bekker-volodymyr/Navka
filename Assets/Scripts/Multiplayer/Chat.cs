using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject content;

    void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, (PhotonNetwork.NickName + " : " + inputField.text));

        inputField.text = "";
    }

    [PunRPC]
    public void GetMessage(string recievedMessage)
    {
        GameObject rMessage = Instantiate(message, Vector3.zero, Quaternion.identity, content.transform);
        rMessage.GetComponent<Message>().myMessage.text = recievedMessage;
    }
}
