using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{

    [SerializeField] private GameObject sound;
    [SerializeField] private string keyName;
    
    // Start is called before the first frame update
    void Start()
    {
        sound.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyName))
        {
            PlaySound();
            Invoke("StopSound", 1.2f);
        }

        //if (Input.GetKeyUp("f"))
        //{
        //    StopSound();
        //}
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
