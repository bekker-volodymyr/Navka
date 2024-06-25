using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractSound : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    [SerializeField] private string keyName;
    [SerializeField] private string keyName2;

    // Start is called before the first frame update
    void Start()
    {
        sound.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyName) || Input.GetKeyDown(keyName2))
        {
            PlaySound();
            Invoke("StopSound", 3.0f);
        }
    }

    void PlaySound()
    {
        sound.SetActive(true);
    }

    void StopSound()
    {
        sound.SetActive(false);
    }  
}
