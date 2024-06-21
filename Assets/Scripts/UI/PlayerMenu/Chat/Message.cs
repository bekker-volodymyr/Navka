using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public TMP_Text myMessage;

    private void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();
    }
}
