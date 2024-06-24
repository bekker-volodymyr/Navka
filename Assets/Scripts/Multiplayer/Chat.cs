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

    public void SendMyMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, (PhotonNetwork.NickName + " : " + inputField.text));

        inputField.text = "";
    }

    [PunRPC]
    public void GetMessage(string recievedMessage)
    {
        GameObject rMessage = Instantiate(message, Vector3.zero, Quaternion.identity, content.transform);
        rMessage.SetActive(true);
        rMessage.GetComponent<Message>().myMessage.text = recievedMessage;

        // Debugging the position and hierarchy
        Debug.Log($"Message instance created at: {rMessage.transform.position}");
        Debug.Log($"Message parent: {rMessage.transform.parent.name}");
        Debug.Log($"Message active state: {rMessage.activeSelf}");
    }
}
