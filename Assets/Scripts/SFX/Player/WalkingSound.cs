using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    [SerializeField] private Player player;
    [SerializeField] private List<string> keyNames;

    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        sound.SetActive(false);
        //lastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        foreach (string keys in keyNames)
        {
            if (Input.GetKeyDown(keys))
            {
                PlaySound();
                //lastPosition = player.transform.position;
            }
            if (Input.GetKeyUp(keys))
            {
                StopSound();
            }
        }


        if (player.StateMachine.CurrentState == player.MoveToPointState)
        {
            PlaySound();
        }
        //if (player.StateMachine.CurrentState != player.MoveToPointState)
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
