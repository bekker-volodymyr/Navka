using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    [SerializeField] private Player player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<string> keyNames;

    //private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        sound.SetActive(false);
        //lastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.magnitude > 0.1f)
        {
            PlaySound();
            //// If the player is moving and the audio is not playing, play the audio
            //if (!audioSource.isPlaying)
            //{
            //    audioSource.Play();
            //}
        }
        else
        {
            StopSound();
            //// If the player is not moving and the audio is playing, stop the audio
            //if (audioSource.isPlaying)
            //{
            //    audioSource.Stop();
            //}
        }
    }


    //foreach (string keys in keyNames)
    //{
    //    if (Input.GetKeyDown(keys))
    //    {
    //        PlaySound();
    //        //lastPosition = player.transform.position;
    //    }
    //    if (Input.GetKeyUp(keys))
    //    {
    //        StopSound();
    //    }
    //}


    //if (player.StateMachine.CurrentState == player.MoveToPointState)
    //{
    //    PlaySound();
    //}
    //if (player.StateMachine.CurrentState != player.MoveToPointState)
    //{
    //    StopSound();
    //}

    void PlaySound()
    {
        sound.SetActive(true);
    }

    void StopSound()
    {
        sound.SetActive(false);
    }
}

