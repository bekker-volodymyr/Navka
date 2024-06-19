using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideoutEntrance : MonoBehaviour, IInteractable
{
    private string sceneName = "HideoutScene";

    private void EnterHideout() 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnInteraction(GameObject interactObject)
    {
        EnterHideout();
    }
}
