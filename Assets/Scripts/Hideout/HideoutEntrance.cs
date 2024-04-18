using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutEntrance : MonoBehaviour, IInteractable
{
    //[SerializeField] private HideoutStruct hideoutData;
    public string sceneName;

    [SerializeField] private TextMeshProUGUI pickUpText;
    private bool pickUpAllowed;


    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void EnterHideout() {
        //Debug.Log("attempt to enter hideout");
        SceneManager.LoadScene(sceneName);
    }

    public void OnInteraction()
    {
        EnterHideout(); // - crash here - says no reference for an object 
    }
}
