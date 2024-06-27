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
     
        }
        else
        {
            StopSound();
         
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

