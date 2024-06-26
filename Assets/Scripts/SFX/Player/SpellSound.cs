using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class SpellSound : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    //[SerializeField] private Player player;
    private UnityEngine.UI.Button spellButton;

    // Start is called before the first frame update
    void Start()
    {
        //spellButton = GameObject.FindAnyObject<>
        sound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //spellButton.onClick.AddListener(OnButtonClick);
        Invoke("StopSound", 3.0f);

    }

    void OnButtonClick()
    {
        sound.SetActive(true);
    }

    void StopSound()
    {
        sound.SetActive(false);
    }
}
