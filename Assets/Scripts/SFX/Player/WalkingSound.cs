using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    //[SerializeField] private Player player;
    [SerializeField] private List<string> keyNames;


    // Start is called before the first frame update
    void Start()
    {
        sound.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKey("w"))
        //{
        //    PlaySound();
        //}

        //if (Input.GetKeyDown("s"))
        //{
        //    PlaySound();
        //}

        //if (Input.GetKeyDown("a"))
        //{
        //    PlaySound();
        //}

        //if (Input.GetKeyDown("d"))
        //{
        //    PlaySound();
        //}

        //if (Input.GetKeyUp("w"))
        //{
        //    StopSound();
        //}

        //if (Input.GetKeyUp("s"))
        //{
        //    StopSound();
        //}

        //if (Input.GetKeyUp("a"))
        //{
        //    StopSound();
        //}

        //if (Input.GetKeyUp("d"))
        //{
        //    StopSound();
        //}



        if (Input.GetKey(keyNames[0]))
        {
            PlaySound();
        }

        if (Input.GetKeyDown(keyNames[1]))
        {
            PlaySound();
        }

        if (Input.GetKeyDown(keyNames[2]))
        {
            PlaySound();
        }

        if (Input.GetKeyDown(keyNames[3]))
        {
            PlaySound();
        }

        if (Input.GetKeyUp(keyNames[0]))
        {
            StopSound();
        }

        if (Input.GetKeyUp(keyNames[1]))
        {
            StopSound();
        }

        if (Input.GetKeyUp(keyNames[2]))
        {
            StopSound();
        }

        if (Input.GetKeyUp(keyNames[3]))
        {
            StopSound();
        }


        //if (player.StateMachine.CurrentState == player.MoveToPointState)
        //{
        //PlaySound();
        //}
        //else
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
