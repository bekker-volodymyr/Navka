using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractHintToggler : MonoBehaviour
{
    [Space]
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
