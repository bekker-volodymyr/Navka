using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutEntrance : MonoBehaviour, IInteractable
{
    //[SerializeField] private HideoutStruct hideoutData;
    public string sceneName;

    private Enums.InteractionType interactType = Enums.InteractionType.EnvironmentObject;
    public Enums.InteractionType InteractionType { get { return interactType; } }

    [SerializeField] private TextMeshProUGUI interactText;
    private bool interactAllowed;

    private void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactText.gameObject.SetActive(true);
            interactAllowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            interactText.gameObject.SetActive(false);
            interactAllowed = false;
        }
    }

    private void EnterHideout() {
        //Debug.Log("attempt to enter hideout");
        SceneManager.LoadScene(sceneName);
    }

    public void OnInteraction(Player player)
    {
        EnterHideout(); // - crash here - says no reference for an object 
    }
}
